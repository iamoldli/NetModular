using System;
using System.IO;
using System.Text;
using Qiniu.Util;
using Qiniu.Http;

namespace Qiniu.Storage
{
    /// <summary>
    /// 空间文件下载，只提供简单下载逻辑
    /// 对于大文件下载、断点续下载等需求，可以根据实际情况自行实现
    /// </summary>
    public class DownloadManager
    {
        /// <summary>
        /// 生成授权的下载链接(访问私有空间中的文件时需要使用这种链接)
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="domain">(私有)空间文件的下载域名</param>
        /// <param name="fileName">（私有）空间文件名</param>
        /// <param name="expireInSeconds">从生成此链接的时刻算起，该链接有效时间(单位:秒)</param>
        /// <returns>已授权的下载链接</returns>
        public static string CreatePrivateUrl(Mac mac, string domain, string fileName, int expireInSeconds = 3600)
        {
            long deadline = UnixTimestamp.GetUnixTimestamp(expireInSeconds);
            string publicUrl = CreatePublishUrl(domain, fileName);
            StringBuilder sb = new StringBuilder(publicUrl);
            if (publicUrl.Contains("?"))
            {
                sb.AppendFormat("&e={0}", deadline);
            }
            else
            {
                sb.AppendFormat("?e={0}", deadline);
            }
            
            string token = Auth.CreateDownloadToken(mac, sb.ToString());
            sb.AppendFormat("&token={0}", token);

            return sb.ToString();
        }

        /// <summary>
        /// 生成公开空间的下载链接
        /// </summary>
        /// <param name="domain">公开空间的文件下载域名</param>
        /// <param name="fileName">公开空间文件名</param>
        /// <returns>公开空间文件下载链接</returns>
        public static string CreatePublishUrl(string domain, string fileName)
        {
            return string.Format("{0}/{1}", domain, Uri.EscapeUriString(fileName));
        }

        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="url">(可访问的或者已授权的)链接</param>
        /// <param name="saveasFile">(另存为)本地文件名</param>
        /// <returns>下载资源的结果</returns>
        public static HttpResult Download(string url, string saveasFile)
        {
            HttpResult result = new HttpResult();

            try
            {
                HttpManager httpManager = new HttpManager();

                result = httpManager.Get(url, null, true);
                if (result.Code == (int)HttpCode.OK)
                {
                    using (FileStream fs = File.Create(saveasFile, result.Data.Length))
                    {
                        fs.Write(result.Data, 0, result.Data.Length);
                        fs.Flush();
                    }
                    result.RefText += string.Format("[{0}] [Download] Success: (Remote file) ==> \"{1}\"\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), saveasFile);
                }
                else
                {
                    result.RefText += string.Format("[{0}] [Download] Error: code = {1}\n",
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"), result.Code);
                }
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}] [Download] Error:  ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
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

            return result;
        }

    }
}
