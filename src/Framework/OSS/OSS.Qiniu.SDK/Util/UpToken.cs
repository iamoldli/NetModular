using System;
using System.Text;
using Newtonsoft.Json;
using Qiniu.Storage;
namespace Qiniu.Util
{
    /// <summary>
    /// 上传凭证工具类
    /// </summary>
    public class UpToken
    {
        /// <summary>
        /// 从上传凭证获取AccessKey
        /// </summary>
        /// <param name="upToken">上传凭证</param>
        /// <returns>AccessKey</returns>
        public static string GetAccessKeyFromUpToken(string upToken)
        {
            string accessKey = null;
            string[] items = upToken.Split(':');
            if (items.Length == 3)
            {
                accessKey = items[0];
            }
            return accessKey;
        }

        /// <summary>
        /// 从上传凭证获取Bucket
        /// </summary>
        /// <param name="upToken">上传凭证</param>
        /// <returns>Bucket</returns>
        public static string GetBucketFromUpToken(string upToken)
        {
            string bucket = null;
            string[] items = upToken.Split(':');
            if (items.Length == 3)
            {
                string encodedPolicy = items[2];
                try
                {
                    string policyStr = Encoding.UTF8.GetString(Base64.UrlsafeBase64Decode(encodedPolicy));
                    PutPolicy putPolicy = JsonConvert.DeserializeObject<PutPolicy>(policyStr);
                    string scope = putPolicy.Scope;
                    string[] scopeItems = scope.Split(':');
                    if (scopeItems.Length >= 1)
                    {
                        bucket = scopeItems[0];
                    }
                }catch(Exception)
                {

                }
            }
            return bucket;
        }
    }
}
