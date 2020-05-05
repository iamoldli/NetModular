using System;
using System.IO;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;

namespace Qiniu.Storage
{
    /// <summary>
    /// 简单上传，适合于以下"情形1":  
    /// (1)网络较好并且待上传的文件体积较小时(比如100MB或更小一点)使用简单上传;
    /// (2)文件较大或者网络状况不理想时请使用分片上传;
    /// (3)文件较大并且需要支持断点续上传，请使用分片上传(断点续上传)
    /// 上传时需要提供正确的上传凭证(由对应的上传策略生成)
    /// 上传策略 http://developer.qiniu.com/article/developer/security/upload-token.html
    /// 上传凭证 http://developer.qiniu.com/article/developer/security/put-policy.html
    /// </summary>
    public class FormUploader
    {
        private Config config;
        private HttpManager httpManager;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config">表单上传的配置信息</param>
        public FormUploader(Config config)
        {
            this.config = config;
            this.httpManager = new HttpManager();
        }

        /// <summary>
        /// 上传文件 - 可附加自定义参数
        /// </summary>
        /// <param name="localFile">待上传的本地文件</param>
        /// <param name="key">要保存的目标文件名称</param>
        /// <param name="token">上传凭证</param>
        /// <param name="extra">上传可选设置</param>
        /// <returns>上传文件后的返回结果</returns>
        public HttpResult UploadFile(string localFile, string key, string token, PutExtra extra)
        {
            try
            {
                FileStream fs = new FileStream(localFile, FileMode.Open);
                return this.UploadStream(fs, key, token, extra);
            }
            catch (Exception ex)
            {
                HttpResult ret = HttpResult.InvalidFile;
                ret.RefText = ex.Message;
                return ret;
            }
        }


        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="data">待上传的数据</param>
        /// <param name="key">要保存的key</param>
        /// <param name="token">上传凭证</param>
        /// <param name="extra">上传可选设置</param>
        /// <returns>上传数据后的返回结果</returns>
        public HttpResult UploadData(byte[] data, string key, string token, PutExtra extra)
        {
            MemoryStream stream = new MemoryStream(data);
            return this.UploadStream(stream, key, token, extra);
        }

        /// <summary>
        /// 上传数据流
        /// </summary>
        /// <param name="stream">(确定长度的)数据流</param>
        /// <param name="key">要保存的key</param>
        /// <param name="token">上传凭证</param>
        /// <param name="extra">上传可选设置</param>
        /// <returns>上传数据流后的返回结果</returns>
        public HttpResult UploadStream(Stream stream, string key, string token, PutExtra putExtra)
        {
            if (putExtra == null)
            {
                putExtra = new PutExtra();
            }
            if (string.IsNullOrEmpty(putExtra.MimeType )) {
                putExtra.MimeType = "application/octet-stream";
            }
            if (putExtra.ProgressHandler == null)
            {
                putExtra.ProgressHandler = DefaultUploadProgressHandler;
            }
            if (putExtra.UploadController == null)
            {
                putExtra.UploadController = DefaultUploadController;
            }
            string fname = key;
            if (string.IsNullOrEmpty(key))
            {
                fname = "fname_temp";
            }

            HttpResult result = new HttpResult();

            try
            {
                string boundary = HttpManager.CreateFormDataBoundary();
                StringBuilder bodyBuilder = new StringBuilder();
                bodyBuilder.AppendLine("--" + boundary);

                if (key != null)
                {
                    //write key when it is not null
                    bodyBuilder.AppendLine("Content-Disposition: form-data; name=\"key\"");
                    bodyBuilder.AppendLine();
                    bodyBuilder.AppendLine(key);
                    bodyBuilder.AppendLine("--" + boundary);
                }

                //write token
                bodyBuilder.AppendLine("Content-Disposition: form-data; name=\"token\"");
                bodyBuilder.AppendLine();
                bodyBuilder.AppendLine(token);
                bodyBuilder.AppendLine("--" + boundary);

                //write extra params
                if (putExtra.Params != null && putExtra.Params.Count > 0)
                {
                    foreach (var p in putExtra.Params)
                    {
                        if (p.Key.StartsWith("x:"))
                        {
                            bodyBuilder.AppendFormat("Content-Disposition: form-data; name=\"{0}\"", p.Key);
                            bodyBuilder.AppendLine();
                            bodyBuilder.AppendLine();
                            bodyBuilder.AppendLine(p.Value);
                            bodyBuilder.AppendLine("--" + boundary);
                        }
                    }
                }

                //prepare data buffer
                int bufferSize = 1024 * 1024;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;
                putExtra.ProgressHandler(0, stream.Length);
                MemoryStream dataMS = new MemoryStream();
                while ((bytesRead = stream.Read(buffer, 0, bufferSize)) != 0)
                {
                    dataMS.Write(buffer, 0, bytesRead);
                }

                //write crc32
                uint crc32 = CRC32.CheckSumBytes(dataMS.ToArray());
                //write key when it is not null
                bodyBuilder.AppendLine("Content-Disposition: form-data; name=\"crc32\"");
                bodyBuilder.AppendLine();
                bodyBuilder.AppendLine(crc32.ToString());
                bodyBuilder.AppendLine("--" + boundary);

                //write fname
                bodyBuilder.AppendFormat("Content-Disposition: form-data; name=\"file\"; filename=\"{0}\"", fname);
                bodyBuilder.AppendLine();

                //write mime type
                bodyBuilder.AppendFormat("Content-Type: {0}", putExtra.MimeType);
                bodyBuilder.AppendLine();
                bodyBuilder.AppendLine();

                //write file data
                StringBuilder bodyEnd = new StringBuilder();
                bodyEnd.AppendLine();
                bodyEnd.AppendLine("--" + boundary + "--");

                byte[] partData1 = Encoding.UTF8.GetBytes(bodyBuilder.ToString());
                byte[] partData2 = dataMS.ToArray();
                byte[] partData3 = Encoding.UTF8.GetBytes(bodyEnd.ToString());

                MemoryStream ms = new MemoryStream();
                ms.Write(partData1, 0, partData1.Length);
                ms.Write(partData2, 0, partData2.Length);
                ms.Write(partData3, 0, partData3.Length);

                //get upload host
                string ak = UpToken.GetAccessKeyFromUpToken(token);
                string bucket = UpToken.GetBucketFromUpToken(token);
                if (ak == null || bucket == null)
                {
                    return HttpResult.InvalidToken;
                }

                string uploadHost = this.config.UpHost(ak, bucket);
                putExtra.ProgressHandler(stream.Length / 5, stream.Length);
                result = httpManager.PostMultipart(uploadHost, ms.ToArray(), boundary, null);
                putExtra.ProgressHandler(stream.Length, stream.Length);
                if (result.Code == (int)HttpCode.OK)
                {
                    result.RefText += string.Format("[{0}] [FormUpload] Uploaded: #STREAM# ==> \"{1}\"\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), key);
                }
                else
                {
                    result.RefText += string.Format("[{0}] [FormUpload] Failed: code = {1}, text = {2}\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), result.Code, result.Text);
                }

                //close memory stream
                ms.Close();
                dataMS.Close();
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [FormUpload] Error: ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
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
            finally
            {
                if (stream != null)
                {
                    try
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception) { }
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
                Console.WriteLine("[{0}] [FormUpload] Progress: {1,7:0.000}%", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0 * uploadedBytes / totalBytes);
            }
            else
            {
                Console.WriteLine("[{0}] [FormUpload] Progress: {1,7:0.000}%\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), 100.0);
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
