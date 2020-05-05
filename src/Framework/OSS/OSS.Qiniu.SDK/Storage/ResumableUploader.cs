using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Qiniu.Util;
using Qiniu.Http;
using Newtonsoft.Json;

namespace Qiniu.Storage
{
    /// <summary>
    /// 分片上传/断点续上传，适合于以下"情形2~3":  
    /// (1)网络较好并且待上传的文件体积较小时(比如100MB或更小一点)使用简单上传;
    /// (2)文件较大或者网络状况不理想时请使用分片上传;
    /// (3)文件较大并且需要支持断点续上传，请使用分片上传(断点续上传)
    /// 上传时需要提供正确的上传凭证(由对应的上传策略生成)
    /// 上传策略 https://developer.qiniu.com/kodo/manual/1206/put-policy
    /// 上传凭证 https://developer.qiniu.com/kodo/manual/1208/upload-token
    /// </summary>
    public class ResumableUploader
    {
        private Config config;
        //分片上传块的大小，固定为4M，不可修改
        private const long BLOCK_SIZE = 4 * 1024 * 1024;
        private const int DEFAULT_MAX_RETRY_TIMES = 3;
        private long CHUNK_SIZE;

        // HTTP请求管理器(GET/POST等)
        private HttpManager httpManager;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config">分片上传的配置信息</param>
        public ResumableUploader(Config config)
        {
            if (config == null)
            {
                this.config = new Config();
            }else
            {
                this.config = config;
            }
            this.httpManager = new HttpManager();
            this.CHUNK_SIZE = ResumeChunk.GetChunkSize(this.config.ChunkSize);
        }


        /// <summary>
        /// 分片上传，支持断点续上传，带有自定义进度处理、高级控制功能
        /// </summary>
        /// <param name="localFile">本地待上传的文件名</param>
        /// <param name="key">要保存的文件名称</param>
        /// <param name="token">上传凭证</param>
        /// <param name="putExtra">上传可选配置</param>
        /// <returns>上传文件后的返回结果</returns>
        public HttpResult UploadFile(string localFile, string key, string token, PutExtra putExtra)
        {
            try
            {
                FileStream fs = new FileStream(localFile, FileMode.Open);
                return this.UploadStream(fs, key, token, putExtra);
            }
            catch (Exception ex)
            {
                HttpResult ret = HttpResult.InvalidFile;
                ret.RefText = ex.Message;
                return ret;
            }
        }

       

        /// <summary>
        /// 分片上传/断点续上传，带有自定义进度处理和上传控制，检查CRC32，可自动重试
        /// </summary>
        /// <param name="stream">待上传文件流</param>
        /// <param name="key">要保存的文件名称</param>
        /// <param name="upToken">上传凭证</param>
        /// <param name="putExtra">可选配置参数</param>
        /// <returns>上传文件后返回结果</returns>
        public HttpResult UploadStream(Stream stream, string key, string upToken, PutExtra putExtra)
        {
            HttpResult result = new HttpResult();

            //check put extra
            if (putExtra == null)
            {
                putExtra = new PutExtra();
            }
            if (putExtra.ProgressHandler == null)
            {
                putExtra.ProgressHandler = DefaultUploadProgressHandler;
            }
            if (putExtra.UploadController == null)
            {
                putExtra.UploadController = DefaultUploadController;
            }
            if (putExtra.MaxRetryTimes == 0)
            {
                putExtra.MaxRetryTimes = DEFAULT_MAX_RETRY_TIMES;
            }

            //start to upload
            try
            {
                long fileSize = stream.Length;
                long chunkSize = CHUNK_SIZE;
                long blockSize = BLOCK_SIZE;
                byte[] chunkBuffer = new byte[chunkSize];
                int blockCount = (int)((fileSize + blockSize - 1) / blockSize);
                int index = 0; // zero block

                //check resume record file
                ResumeInfo resumeInfo = null;
                if (File.Exists(putExtra.ResumeRecordFile))
                {
                    bool useLastRecord = false;
                    resumeInfo = ResumeHelper.Load(putExtra.ResumeRecordFile);
                    if (resumeInfo != null && fileSize==resumeInfo.FileSize)
                    {
                        //check whether ctx expired
                        if (!UnixTimestamp.IsContextExpired(resumeInfo.ExpiredAt))
                        {
                            useLastRecord = true;
                        }
                    }

                    if (useLastRecord)
                    {
                        index = resumeInfo.BlockIndex;
                    }
                }
                if (resumeInfo == null)
                {
                    resumeInfo = new ResumeInfo()
                    {
                        FileSize = fileSize,
                        BlockIndex = 0,
                        BlockCount = blockCount,
                        Contexts = new string[blockCount],
                        ExpiredAt = 0,
                    };
                }

                //read from offset
                long offset = index * blockSize;
                string context = null;
                long expiredAt = 0;
                long leftBytes = fileSize - offset;
                long blockLeft = 0;
                long blockOffset = 0;
                HttpResult hr = null;
                ResumeContext rc = null;

                stream.Seek(offset, SeekOrigin.Begin);

                var upts = UploadControllerAction.Activated;
                bool bres = true;
                var manualResetEvent = new ManualResetEvent(true);
                int iTry = 0;

                while (leftBytes > 0)
                {
                    // 每上传一个BLOCK之前，都要检查一下UPTS
                    upts = putExtra.UploadController();

                    if (upts == UploadControllerAction.Aborted)
                    {
                        result.Code = (int)HttpCode.USER_CANCELED;
                        result.RefCode = (int)HttpCode.USER_CANCELED;
                        result.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is aborted\n",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));

                        return result;
                    }
                    else if (upts == UploadControllerAction.Suspended)
                    {
                        if (bres)
                        {
                            bres = false;
                            manualResetEvent.Reset();

                            result.RefCode = (int)HttpCode.USER_PAUSED;
                            result.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is paused\n",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                        }
                        manualResetEvent.WaitOne(1000);
                    }
                    else
                    {
                        if (!bres)
                        {
                            bres = true;
                            manualResetEvent.Set();

                            result.RefCode = (int)HttpCode.USER_RESUMED;
                            result.RefText += string.Format("[{0}] [ResumableUpload] Info: upload task is resumed\n",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                        }

                        #region one-block

                        #region mkblk
                        if (leftBytes < BLOCK_SIZE)
                        {
                            blockSize = leftBytes;
                        }
                        else
                        {
                            blockSize = BLOCK_SIZE;
                        }

                        if (leftBytes < CHUNK_SIZE)
                        {
                            chunkSize = leftBytes;
                        }
                        else
                        {
                            chunkSize = CHUNK_SIZE;
                        }

                        //read data buffer
                        stream.Read(chunkBuffer, 0, (int)chunkSize);

                        iTry = 0;
                        while (++iTry <= putExtra.MaxRetryTimes)
                        {
                            result.RefText += string.Format("[{0}] [ResumableUpload] try mkblk#{1}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), iTry);

                            hr = MakeBlock(chunkBuffer, blockSize, chunkSize, upToken);
                            if (hr.Code == (int)HttpCode.OK && hr.RefCode != (int)HttpCode.USER_NEED_RETRY)
                            {
                                break;
                            }
                        }
                       
                        if (hr.Code != (int)HttpCode.OK || hr.RefCode == (int)HttpCode.USER_NEED_RETRY)
                        {
                            result.Shadow(hr);
                            result.RefText += string.Format("[{0}] [ResumableUpload] Error: mkblk: code = {1}, text = {2}, offset = {3}, blockSize = {4}, chunkSize = {5}\n",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), hr.Code, hr.Text, offset, blockSize, chunkSize);

                            return result;
                        }

                        if ((rc = JsonConvert.DeserializeObject<ResumeContext>(hr.Text)) == null)
                        {
                            result.Shadow(hr);
                            result.RefCode = (int)HttpCode.USER_UNDEF;
                            result.RefText += string.Format("[{0}] [ResumableUpload] mkblk Error: JSON Decode Error: text = {1}\n",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), hr.Text);

                            return result;
                        }

                        context = rc.Ctx;
                        offset += chunkSize;
                        leftBytes -= chunkSize;

                        #endregion mkblk
                        putExtra.ProgressHandler(offset, fileSize);
                        
                        if (leftBytes > 0)
                        {
                            blockLeft = blockSize - chunkSize;
                            blockOffset = chunkSize;
                            while (blockLeft > 0)
                            {
                                #region bput-loop

                                if (blockLeft < CHUNK_SIZE)
                                {
                                    chunkSize = blockLeft;
                                }
                                else
                                {
                                    chunkSize = CHUNK_SIZE;
                                }

                                stream.Read(chunkBuffer, 0, (int)chunkSize);

                                iTry = 0;
                                while (++iTry <= putExtra.MaxRetryTimes)
                                {
                                    result.RefText += string.Format("[{0}] [ResumableUpload] try bput#{1}\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), iTry);

                                    hr = BputChunk(chunkBuffer, blockOffset, chunkSize, context, upToken);

                                    if (hr.Code == (int)HttpCode.OK && hr.RefCode != (int)HttpCode.USER_NEED_RETRY)
                                    {
                                        break;
                                    }
                                }
                                if (hr.Code != (int)HttpCode.OK || hr.RefCode == (int)HttpCode.USER_NEED_RETRY)
                                {
                                    result.Shadow(hr);
                                    result.RefText += string.Format("[{0}] [ResumableUpload] Error: bput: code = {1}, text = {2}, offset = {3}, blockOffset = {4}, chunkSize = {5}\n",
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), hr.Code, hr.Text, offset, blockOffset, chunkSize);

                                    return result;
                                }

                                if ((rc=JsonConvert.DeserializeObject<ResumeContext>(hr.Text))==null)
                                {
                                    result.Shadow(hr);
                                    result.RefCode = (int)HttpCode.USER_UNDEF;
                                    result.RefText += string.Format("[{0}] [ResumableUpload] bput Error: JSON Decode Error: text = {1}\n",
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), hr.Text);

                                    return result;
                                }

                                context = rc.Ctx;
                                if (expiredAt == 0)
                                {
                                    expiredAt = rc.Expired_At;
                                }

                                offset += chunkSize;
                                leftBytes -= chunkSize;
                                blockOffset += chunkSize;
                                blockLeft -= chunkSize;
                                #endregion bput-loop

                                putExtra.ProgressHandler(offset, fileSize);
                            }
                        }

                        #endregion one-block

                        resumeInfo.BlockIndex = index;
                        resumeInfo.Contexts[index] = context;
                        resumeInfo.ExpiredAt = expiredAt;
                        if (!string.IsNullOrEmpty(putExtra.ResumeRecordFile))
                        {
                            ResumeHelper.Save(resumeInfo, putExtra.ResumeRecordFile);
                        }
                        ++index;
                    }
                }

                hr = MakeFile(key, fileSize, key, upToken, putExtra, resumeInfo.Contexts);
                if (hr.Code != (int)HttpCode.OK)
                {
                    result.Shadow(hr);
                    result.RefText += string.Format("[{0}] [ResumableUpload] Error: mkfile: code = {1}, text = {2}\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), hr.Code, hr.Text);

                    return result;
                }

                if (File.Exists(putExtra.ResumeRecordFile))
                {
                    File.Delete(putExtra.ResumeRecordFile);
                }
                result.Shadow(hr);
                result.RefText += string.Format("[{0}] [ResumableUpload] Uploaded: \"{1}\" ==> \"{2}\"\n",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), putExtra.ResumeRecordFile, key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [ResumableUpload] Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.RefCode = (int)HttpCode.USER_UNDEF;
                result.RefText += sb.ToString();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }

            return result;
        }

        /// <summary>
        /// 根据已上传的所有分片数据创建文件
        /// </summary>
        /// <param name="fileName">源文件名</param>
        /// <param name="size">文件大小</param>
        /// <param name="key">要保存的文件名</param>
        /// <param name="contexts">所有数据块的Context</param>
        /// <param name="upToken">上传凭证</param>
        /// <param name="putExtra">用户指定的额外参数</param>
        /// <returns>此操作执行后的返回结果</returns>
        private HttpResult MakeFile(string fileName, long size, string key, string upToken, PutExtra putExtra, string[] contexts)
        {
            HttpResult result = new HttpResult();

            try
            {
                string fnameStr = "fname";
                string mimeTypeStr = "";
                string keyStr = "";
                string paramStr = "";
                //check file name
                if (!string.IsNullOrEmpty(fileName))
                {
                   fnameStr = string.Format("/fname/{0}", Base64.UrlSafeBase64Encode(fileName));
                }

                //check mime type
                if (!string.IsNullOrEmpty(putExtra.MimeType))
                {
                    mimeTypeStr = string.Format("/mimeType/{0}", Base64.UrlSafeBase64Encode(putExtra.MimeType));
                }

                //check key
                if (!string.IsNullOrEmpty(key))
                {
                      keyStr = string.Format("/key/{0}", Base64.UrlSafeBase64Encode(key));
                }
                
                //check extra params
                if (putExtra.Params != null && putExtra.Params.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var kvp in putExtra.Params)
                    {
                        string k = kvp.Key;
                        string v = kvp.Value;
                        if (k.StartsWith("x:") && !string.IsNullOrEmpty(v))
                        {
                            sb.AppendFormat("/{0}/{1}", k,v);
                        }
                    }

                    paramStr = sb.ToString();
                }

                //get upload host
                string ak = UpToken.GetAccessKeyFromUpToken(upToken);
                string bucket = UpToken.GetBucketFromUpToken(upToken);
                if (ak == null || bucket == null)
                {
                    return HttpResult.InvalidToken;
                }

                string uploadHost = this.config.UpHost(ak, bucket);
            
                string url = string.Format("{0}/mkfile/{1}{2}{3}{4}{5}", uploadHost, size, mimeTypeStr, fnameStr, keyStr, paramStr);
                string body = string.Join(",", contexts);
                string upTokenStr = string.Format("UpToken {0}", upToken);

                result = httpManager.PostText(url, body, upTokenStr);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] mkfile Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                if (ex is QiniuException)
                {
                    QiniuException qex = (QiniuException)ex;
                    result.Code = qex.HttpResult.Code;
                    result.RefCode = qex.HttpResult.Code;
                    result.Text = qex.HttpResult.Text;
                    result.RefText += sb.ToString();
                }
                else
                {
                    result.RefCode = (int)HttpCode.USER_UNDEF;
                    result.RefText += sb.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 创建块(携带首片数据),同时检查CRC32
        /// </summary>
        /// <param name="chunkBuffer">数据片，此操作都会携带第一个数据片</param>
        /// <param name="blockSize">块大小，除了最后一块可能不足4MB，前面的所有数据块恒定位4MB</param>
        /// <param name="chunkSize">分片大小，一个块可以被分为若干片依次上传然后拼接或者不分片直接上传整块</param>
        /// <param name="upToken">上传凭证</param>
        /// <returns>此操作执行后的返回结果</returns>
        private HttpResult MakeBlock(byte[] chunkBuffer, long blockSize, long chunkSize, string upToken)
        {
            HttpResult result = new HttpResult();

            try
            {
                //get upload host
                string ak = UpToken.GetAccessKeyFromUpToken(upToken);
                string bucket = UpToken.GetBucketFromUpToken(upToken);
                if (ak == null || bucket == null)
                {
                    return HttpResult.InvalidToken;
                }

                string uploadHost = this.config.UpHost(ak, bucket);

                string url = string.Format("{0}/mkblk/{1}", uploadHost, blockSize);
                string upTokenStr = string.Format("UpToken {0}", upToken);
                using (MemoryStream ms = new MemoryStream(chunkBuffer, 0, (int)chunkSize))
                {
                    byte[] data = ms.ToArray();

                    result = httpManager.PostData(url, data, upTokenStr);

                    if (result.Code == (int)HttpCode.OK)
                    {
                        ResumeContext rc = JsonConvert.DeserializeObject<ResumeContext>(result.Text);

                        if (rc.Crc32 > 0)
                        {
                            uint crc_1 = rc.Crc32;
                            uint crc_2 = CRC32.CheckSumSlice(chunkBuffer, 0, (int)chunkSize);
                            if (crc_1 != crc_2)
                            {
                                result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                                result.RefText += string.Format(" CRC32: remote={0}, local={1}\n", crc_1, crc_2);
                            }
                        }
                        else
                        {
                            result.RefText += string.Format("[{0}] JSON Decode Error: text = {1}",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), result.Text);
                            result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                        }
                    }
                    else
                    {
                        result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] mkblk Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                if (ex is QiniuException)
                {
                    QiniuException qex = (QiniuException)ex;
                    result.Code = qex.HttpResult.Code;
                    result.RefCode = qex.HttpResult.Code;
                    result.Text = qex.HttpResult.Text;
                    result.RefText += sb.ToString();
                }
                else
                {
                    result.RefCode = (int)HttpCode.USER_UNDEF;
                    result.RefText += sb.ToString();
                }
            }

            return result;
        }


        /// <summary>
        /// 上传数据片,同时检查CRC32
        /// </summary>
        /// <param name="chunkBuffer">数据片</param>
        /// <param name="offset">当前片在块中的偏移位置</param>
        /// <param name="chunkSize">当前片的大小</param>
        /// <param name="context">承接前一片数据用到的Context</param>
        /// <param name="upToken">上传凭证</param>
        /// <returns>此操作执行后的返回结果</returns>
        private HttpResult BputChunk(byte[] chunkBuffer, long offset, long chunkSize, string context, string upToken)
        {
            HttpResult result = new HttpResult();

            try
            {
                //get upload host
                string ak = UpToken.GetAccessKeyFromUpToken(upToken);
                string bucket = UpToken.GetBucketFromUpToken(upToken);
                if (ak == null || bucket == null)
                {
                    return HttpResult.InvalidToken;
                }

                string uploadHost = this.config.UpHost(ak, bucket);

                string url = string.Format("{0}/bput/{1}/{2}", uploadHost, context, offset);
                string upTokenStr = string.Format("UpToken {0}", upToken);

                using (MemoryStream ms = new MemoryStream(chunkBuffer, 0, (int)chunkSize))
                {
                    byte[] data = ms.ToArray();

                    result = httpManager.PostData(url, data, upTokenStr);

                    if (result.Code == (int)HttpCode.OK)
                    {
                       Dictionary<string, string> rd=JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Text);
                        if (rd.ContainsKey("crc32"))
                        {
                            uint crc_1 = Convert.ToUInt32(rd["crc32"]);
                            uint crc_2 = CRC32.CheckSumSlice(chunkBuffer, 0, (int)chunkSize);
                            if (crc_1 != crc_2)
                            {
                                result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                                result.RefText += string.Format(" CRC32: remote={0}, local={1}\n", crc_1, crc_2);
                            }
                        }
                        else
                        {
                            result.RefText += string.Format("[{0}] JSON Decode Error: text = {1}",
                                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), result.Text);
                            result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                        }
                    }
                    else
                    {
                        result.RefCode = (int)HttpCode.USER_NEED_RETRY;
                    }
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] bput Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                if (ex is QiniuException)
                {
                    QiniuException qex = (QiniuException)ex;
                    result.Code = qex.HttpResult.Code;
                    result.RefCode = qex.HttpResult.Code;
                    result.Text = qex.HttpResult.Text;
                    result.RefText += sb.ToString();
                }
                else
                {
                    result.RefCode = (int)HttpCode.USER_UNDEF;
                    result.RefText += sb.ToString();
                }
            }

            return result;
        }

        /// <summary>
        /// 默认的进度处理函数-上传文件
        /// </summary>
        /// <param name="uploadedBytes">已上传的字节数</param>
        /// <param name="totalBytes">文件总字节数</param>
        public static void DefaultUploadProgressHandler(long uploadedBytes, long totalBytes)
        {
            if (uploadedBytes < totalBytes)
            {
                Console.WriteLine("[{0}] [ResumableUpload] Progress: {1,7:0.000}%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0 * uploadedBytes / totalBytes);
            }
            else
            {
                Console.WriteLine("[{0}] [ResumableUpload] Progress: {1,7:0.000}%\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0);
            }
        }
      
        /// <summary>
        /// 默认的上传控制函数，默认不执行任何控制
        /// </summary>
        /// <returns>控制状态</returns>
        public static UploadControllerAction DefaultUploadController()
        {
            return UploadControllerAction.Activated;
        }
    }
}
