namespace Qiniu.Util
{
    /// <summary>
    /// Authentication/Authorization
    /// </summary>
    public class Auth
    {
        private Signature signature;

        /// <summary>
        /// 一般初始化
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        public Auth(Mac mac)
        {
            signature = new Signature(mac);
        }

        /// <summary>
        /// 生成管理凭证
        /// 有关管理凭证请参阅
        /// http://developer.qiniu.com/article/developer/security/access-token.html
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="body">请求的主体内容</param>
        /// <returns>生成的管理凭证</returns>
        public string CreateManageToken(string url,byte[] body)
        {
            return string.Format("QBox {0}", signature.SignRequest(url, body));
        }

        /// <summary>
        /// 生成管理凭证-不包含body
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <returns>生成的管理凭证</returns>
        public string CreateManageToken(string url)
        {
            return CreateManageToken(url, null);
        }

        /// <summary>
        /// 生成上传凭证
        /// </summary>
        /// <param name="jsonStr">上传策略对应的JSON字符串</param>
        /// <returns>生成的上传凭证</returns>
        public string CreateUploadToken(string jsonStr)
        {
            return signature.SignWithData(jsonStr);
        }

        /// <summary>
        /// 生成下载凭证
        /// </summary>
        /// <param name="url">原始链接</param>
        /// <returns></returns>
        public string CreateDownloadToken(string url)
        {
            return signature.Sign(url);
        }

        /// <summary>
        /// 生成推流地址使用的凭证
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CreateStreamPublishToken(string path)
        {
            return signature.Sign(path);
        }

        /// <summary>
        /// 生成流管理凭证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string CreateStreamManageToken(string data)
        {
            return string.Format("Qiniu {0}", signature.SignWithData(data));
        }

        #region STATIC

        /// <summary>
        /// 生成管理凭证
        /// 有关管理凭证请参阅
        /// http://developer.qiniu.com/article/developer/security/access-token.html
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="url">访问的URL</param>
        /// <param name="body">请求的body</param>
        /// <returns>生成的管理凭证</returns>
        public static string CreateManageToken(Mac mac, string url, byte[] body)
        {
            Signature sx = new Signature(mac);
            return string.Format("QBox {0}", sx.SignRequest(url, body));
        }

        /// <summary>
        /// 生成管理凭证-不包含body
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="url">请求的URL</param>
        /// <returns>生成的管理凭证</returns>
        public static string CreateManageToken(Mac mac, string url)
        {
            return CreateManageToken(mac, url, null);
        }

        /// <summary>
        /// 生成上传凭证
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="jsonBody">上传策略JSON串</param>
        /// <returns>生成的上传凭证</returns>
        public static string CreateUploadToken(Mac mac, string jsonBody)
        {
            Signature sx = new Signature(mac);
            return sx.SignWithData(jsonBody);
        }

        /// <summary>
        /// 生成下载凭证
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="url">原始链接</param>
        /// <returns></returns>
        public static string CreateDownloadToken(Mac mac, string url)
        {
            Signature sx = new Signature(mac);
            return sx.Sign(url);
        }

        /// <summary>
        /// 生成推流地址使用的凭证
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="path">URL路径</param>
        /// <returns></returns>
        public static string CreateStreamPublishToken(Mac mac,string path)
        {
            Signature sx = new Signature(mac);
            return sx.Sign(path);
        }

        /// <summary>
        /// 生成流管理凭证
        /// </summary>
        /// <param name="mac">账号(密钥)</param>
        /// <param name="data">待签数据</param>
        /// <returns></returns>
        public static string CreateStreamManageToken(Mac mac, string data)
        {
            Signature sx = new Signature(mac);
            return string.Format("Qiniu {0}", sx.Sign(data));
        }

        #endregion STATIC

    }
}
