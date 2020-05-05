using System;
using System.Text;
using Qiniu.Http;
using Qiniu.Util;
using System.Collections.Generic;
namespace Qiniu.Storage
{
    /// <summary>
    /// 空间(资源)管理/操作
    /// </summary>
    public class BucketManager
    {
        private Mac mac;
        private Auth auth;
        private HttpManager httpManager;
        private Config config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="config"></param>
        public BucketManager(Mac mac, Config config)
        {
            this.mac = mac;
            this.auth = new Auth(mac);
            this.httpManager = new HttpManager();
            this.config = config;
        }


        /// <summary>
        /// 获取空间文件信息
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>文件信息获取结果</returns>
        public StatResult Stat(string bucket, string key)
        {
            StatResult result = new StatResult();

            try
            {
                string statUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, bucket),
                    StatOp(bucket, key));
                string token = auth.CreateManageToken(statUrl);

                HttpResult hr = httpManager.Get(statUrl, token);
                result.Shadow(hr);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [stat] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 获取空间(bucket)列表
        /// </summary>
        /// <param name="shared">是否列出被授权访问的空间</param>
        /// <returns>空间列表获取结果</returns>
        public BucketsResult Buckets(bool shared)
        {
            BucketsResult result = new BucketsResult();

            try
            {
                string scheme = this.config.UseHttps ? "https://" : "http://";
                string rsHost = string.Format("{0}{1}", scheme, Config.DefaultRsHost);
                string sharedStr = "false";
                if (shared)
                {
                    sharedStr = "true";
                }
                string bucketsUrl = string.Format("{0}/buckets?shared={1}", rsHost, sharedStr);
                string token = auth.CreateManageToken(bucketsUrl);

                HttpResult hr = httpManager.Get(bucketsUrl, token);
                result.Shadow(hr);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [buckets] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Delete(string bucket, string key)
        {
            HttpResult result = new HttpResult();

            try
            {
                string deleteUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, bucket),
                    DeleteOp(bucket, key));
                string token = auth.CreateManageToken(deleteUrl);

                result = httpManager.Post(deleteUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [delete] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标key</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Copy(string srcBucket, string srcKey, string dstBucket, string dstKey)
        {
            return Copy(srcBucket, srcKey, dstBucket, dstKey, false);
        }

        /// <summary>
        /// 复制文件 (with 'force' param)
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标key</param>
        /// <param name="force">force标志,true/false</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Copy(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
        {
            HttpResult result = new HttpResult();

            try
            {
                string copyUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, srcBucket),
                    CopyOp(srcBucket, srcKey, dstBucket, dstKey, force));
                string token = auth.CreateManageToken(copyUrl);

                result = httpManager.Post(copyUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [copy] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标key</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Move(string srcBucket, string srcKey, string dstBucket, string dstKey)
        {
            return Move(srcBucket, srcKey, dstBucket, dstKey, false);
        }

        /// <summary>
        /// 移动文件 (with 'force' param)
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标key</param>
        /// <param name="force">force标志,true/false</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Move(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
        {
            HttpResult result = new HttpResult();

            try
            {
                string moveUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, srcBucket),
                    MoveOp(srcBucket, srcKey, dstBucket, dstKey, force));
                string token = auth.CreateManageToken(moveUrl);

                result = httpManager.Post(moveUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [move] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 修改文件MimeType
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="mimeType">修改后的MIME Type</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult ChangeMime(string bucket, string key, string mimeType)
        {
            HttpResult result = new HttpResult();

            try
            {
                string chgmUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, bucket),
                    ChangeMimeOp(bucket, key, mimeType));
                string token = auth.CreateManageToken(chgmUrl);
                result = httpManager.Post(chgmUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [chgm] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 修改文件存储类型
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="fileType">修改后的文件存储类型，0表示普通存储，1表示低频存储</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult ChangeType(string bucket, string key, int fileType)
        {
            HttpResult result = new HttpResult();

            try
            {
                string chtypeUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, bucket),
                    ChangeTypeOp(bucket, key, fileType));
                string token = auth.CreateManageToken(chtypeUrl);
                result = httpManager.Post(chtypeUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [chtype] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 批处理
        /// </summary>
        /// <param name="batchOps">批量操作的操作字符串</param>
        /// <returns>状态码为200时表示OK</returns>
        private BatchResult Batch(string batchOps)
        {
            BatchResult result = new BatchResult();

            try
            {
                string scheme = this.config.UseHttps ? "https://" : "http://";
                string rsHost = string.Format("{0}{1}", scheme, Config.DefaultRsHost);
                string batchUrl = rsHost + "/batch";
                byte[] data = Encoding.UTF8.GetBytes(batchOps);
                string token = auth.CreateManageToken(batchUrl, data);

                HttpResult hr = httpManager.PostForm(batchUrl, data, token);
                result.Shadow(hr);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [batch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 批处理，字符串数组拼接后与另一形式等价
        /// </summary>
        /// <param name="ops">批量操作的操作字符串数组</param>
        /// <returns>状态码为200时表示OK</returns>
        public BatchResult Batch(IList<string> ops)
        {
            StringBuilder opsb = new StringBuilder();
            opsb.AppendFormat("op={0}", ops[0]);
            for (int i = 1; i < ops.Count; ++i)
            {
                opsb.AppendFormat("&op={0}", ops[i]);
            }

            return Batch(opsb.ToString());
        }

        /// <summary>
        /// 抓取文件
        /// </summary>
        /// <param name="resUrl">资源URL</param>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>状态码为200时表示OK</returns>
        public FetchResult Fetch(string resUrl, string bucket, string key)
        {
            FetchResult result = new FetchResult();

            try
            {
                string fetchUrl = string.Format("{0}{1}", this.config.IovipHost(this.mac.AccessKey, bucket),
                    FetchOp(resUrl, bucket, key));
                string token = auth.CreateManageToken(fetchUrl);

                HttpResult httpResult = httpManager.Post(fetchUrl, token);
                result.Shadow(httpResult);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [fetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 更新文件，适用于"镜像源站"设置的空间
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult Prefetch(string bucket, string key)
        {
            HttpResult result = new HttpResult();

            try
            {
                string prefetchUrl = this.config.IovipHost(this.mac.AccessKey, bucket) + PrefetchOp(bucket, key);
                string token = auth.CreateManageToken(prefetchUrl);

                result = httpManager.Post(prefetchUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [prefetch] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 获取空间的域名
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <returns>空间对应的域名</returns>
        public DomainsResult Domains(string bucket)
        {
            DomainsResult result = new DomainsResult();

            try
            {
                string scheme = this.config.UseHttps ? "https://" : "http://";
                string rsHost = string.Format("{0}{1}", scheme, Config.DefaultApiHost);
                string domainsUrl = string.Format("{0}{1}", rsHost, "/v6/domain/list");
                string body = string.Format("tbl={0}", bucket);
                byte[] data = Encoding.UTF8.GetBytes(body);
                string token = auth.CreateManageToken(domainsUrl, data);

                HttpResult hr = httpManager.PostForm(domainsUrl, data, token);
                result.Shadow(hr);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [domains] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 
        /// 获取空间文件列表 
        /// listFiles(bucket, prefix, marker, limit, delimiter)
        /// 
        /// bucket:    目标空间名称
        /// 
        /// prefix:    返回指定文件名前缀的文件列表(prefix可设为null)
        /// 
        /// marker:    考虑到设置limit后返回的文件列表可能不全(需要重复执行listFiles操作)
        ///            执行listFiles操作时使用marker标记来追加新的结果
        ///            特别注意首次执行listFiles操作时marker为null
        ///            
        /// limit:     每次返回结果所包含的文件总数限制(limit最大值1000，建议值100)
        /// 
        /// delimiter: 分隔符，比如-或者/等等，可以模拟作为目录结构(参考下述示例)
        ///            假设指定空间中有2个文件 fakepath/1.txt fakepath/2.txt
        ///            现设置分隔符delimiter = / 得到返回结果items =[]，commonPrefixes = [fakepath/]
        ///            然后调整prefix = fakepath/ delimiter = null 得到所需结果items = [1.txt,2.txt]
        ///            于是可以在本地先创建一个目录fakepath,然后在该目录下写入items中的文件
        ///            
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="prefix">前缀</param>
        /// <param name="marker">标记</param>
        /// <param name="limit">数量限制</param>
        /// <param name="delimiter">分隔符</param>
        /// <returns>文件列表获取结果</returns>
        public ListResult ListFiles(string bucket, string prefix, string marker, int limit, string delimiter)
        {
            ListResult result = new ListResult();

            try
            {
                StringBuilder sb = new StringBuilder("/list?bucket=" + bucket);

                if (!string.IsNullOrEmpty(marker))
                {
                    sb.Append("&marker=" + marker);
                }

                if (!string.IsNullOrEmpty(prefix))
                {
                    sb.Append("&prefix=" + prefix);
                }

                if (!string.IsNullOrEmpty(delimiter))
                {
                    sb.Append("&delimiter=" + delimiter);
                }

                if (limit > 1000 || limit < 1)
                {
                    sb.Append("&limit=1000");
                }
                else
                {
                    sb.Append("&limit=" + limit);
                }

                string listUrl = string.Format("{0}{1}", this.config.RsfHost(this.mac.AccessKey, bucket), sb.ToString());
                string token = auth.CreateManageToken(listUrl);

                HttpResult hr = httpManager.Post(listUrl, token);
                result.Shadow(hr);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [listFiles] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 更新文件生命周期
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="deleteAfterDays">多少天后删除</param>
        /// <returns>状态码为200时表示OK</returns>
        public HttpResult DeleteAfterDays(string bucket, string key, int deleteAfterDays)
        {
            HttpResult result = new HttpResult();

            try
            {
                string updateUrl = string.Format("{0}{1}", this.config.RsHost(this.mac.AccessKey, bucket),
                    DeleteAfterDaysOp(bucket, key, deleteAfterDays));
                string token = auth.CreateManageToken(updateUrl);
                result = httpManager.Post(updateUrl, token);
            }
            catch (QiniuException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [deleteAfterDays] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                Exception e = ex;
                while (e != null)
                {
                    sb.Append(e.Message + " ");
                    e = e.InnerException;
                }
                sb.AppendLine();

                result.Code = ex.HttpResult.Code;
                result.RefCode = ex.HttpResult.Code;
                result.Text = ex.HttpResult.Text;
                result.RefText += sb.ToString();
            }

            return result;
        }

        /// <summary>
        /// 生成stat操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>stat操作字符串</returns>
        public string StatOp(string bucket, string key)
        {
            return string.Format("/stat/{0}", Base64.UrlSafeBase64Encode(bucket, key));
        }

        /// <summary>
        /// 生成delete操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>delete操作字符串</returns>
        public string DeleteOp(string bucket, string key)
        {
            return string.Format("/delete/{0}", Base64.UrlSafeBase64Encode(bucket, key));
        }

        /// <summary>
        /// 生成copy操作字符串
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标文件key</param>
        /// <returns>copy操作字符串</returns>
        public string CopyOp(string srcBucket, string srcKey, string dstBucket, string dstKey)
        {
            return CopyOp(srcBucket, srcKey, dstBucket, dstKey, false);
        }

        /// <summary>
        /// 生成copy(with 'force' param)操作字符串
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标文件key</param>
        /// <param name="force">force标志,true/false</param>
        /// <returns>copy操作字符串</returns>
        public string CopyOp(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
        {
            string fx = force ? "force/true" : "force/false";
            return string.Format("/copy/{0}/{1}/{2}", Base64.UrlSafeBase64Encode(srcBucket, srcKey),
                Base64.UrlSafeBase64Encode(dstBucket, dstKey), fx);
        }

        /// <summary>
        /// 生成move操作字符串
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标文件key</param>
        /// <returns>move操作字符串</returns>
        public string MoveOp(string srcBucket, string srcKey, string dstBucket, string dstKey)
        {
            return MoveOp(srcBucket, srcKey, dstBucket, dstKey, false);
        }

        /// <summary>
        /// 生成copy(with 'force' param)操作字符串
        /// </summary>
        /// <param name="srcBucket">源空间</param>
        /// <param name="srcKey">源文件key</param>
        /// <param name="dstBucket">目标空间</param>
        /// <param name="dstKey">目标文件key</param>
        /// <param name="force">force标志,true/false</param>
        /// <returns>move操作字符串</returns>
        public string MoveOp(string srcBucket, string srcKey, string dstBucket, string dstKey, bool force)
        {
            string fx = force ? "force/true" : "force/false";
            return string.Format("/move/{0}/{1}/{2}", Base64.UrlSafeBase64Encode(srcBucket, srcKey),
                Base64.UrlSafeBase64Encode(dstBucket, dstKey), fx);
        }

        /// <summary>
        /// 生成chgm操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="mimeType">修改后MIME Type</param>
        /// <returns>chgm操作字符串</returns>
        public string ChangeMimeOp(string bucket, string key, string mimeType)
        {
            return string.Format("/chgm/{0}/mime/{1}", Base64.UrlSafeBase64Encode(bucket, key),
                Base64.UrlSafeBase64Encode(mimeType));
        }

        /// <summary>
        /// 生成chtype操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="fileType">修改后文件类型</param>
        /// <returns>chtype操作字符串</returns>
        public string ChangeTypeOp(string bucket, string key, int fileType)
        {
            return string.Format("/chtype/{0}/type/{1}", Base64.UrlSafeBase64Encode(bucket, key),
                fileType);
        }

        /// <summary>
        /// 生成fetch操作字符串
        /// </summary>
        /// <param name="url">资源URL</param>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>fetch操作字符串</returns>
        public string FetchOp(string url, string bucket, string key)
        {
            string entry = null;
            if (key == null)
            {
                entry = Base64.UrlSafeBase64Encode(bucket);
            }
            else
            {
                entry = Base64.UrlSafeBase64Encode(bucket, key);
            }
            return string.Format("/fetch/{0}/to/{1}", Base64.UrlSafeBase64Encode(url), entry);
        }

        /// <summary>
        /// 生成prefetch操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <returns>prefetch操作字符串</returns>
        public string PrefetchOp(string bucket, string key)
        {
            return string.Format("/prefetch/{0}", Base64.UrlSafeBase64Encode(bucket, key));
        }

        /// <summary>
        /// 生成updateLifecycle操作字符串
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">文件key</param>
        /// <param name="deleteAfterDays">多少天后删除(设为0表示取消)</param>
        /// <returns>updateLifecycle操作字符串</returns>
        public string DeleteAfterDaysOp(string bucket, string key, int deleteAfterDays)
        {
            return string.Format("/deleteAfterDays/{0}/{1}",
                Base64.UrlSafeBase64Encode(bucket, key), deleteAfterDays);
        }

    }
}
