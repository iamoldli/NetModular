using System.IO;
using System.Security.Cryptography;
using System.Text;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Lib.Utils.Core.Encrypt
{
    /// <summary>
    /// MD5
    /// </summary>
    public class Md5Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密字符串</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(string source, bool lowerCase = false)
        {
            if (source.IsNull())
                return null;

            return Encrypt(Encoding.UTF8.GetBytes(source), lowerCase);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="source">加密字节流</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(byte[] source, bool lowerCase = false)
        {
            if (source == null)
                return null;

            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(source).ToHex(lowerCase);
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="inputStream">流</param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string Encrypt(Stream inputStream, bool lowerCase = false)
        {
            if (inputStream == null) return null;

            using (var md5Hash = MD5.Create())
            {
                return md5Hash.ComputeHash(inputStream).ToHex(lowerCase);
            }
        }
    }
}
