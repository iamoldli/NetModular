using System.Text;
#if WINDOWS_UWP
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
#else
using System.Security.Cryptography;
#endif

namespace Qiniu.Util
{
    /// <summary>
    /// 计算hash值
    /// 特别注意，不同平台使用的Cryptography可能略有不同，使用中如有遇到问题，请反馈
    /// 提交您的issue到 https://github.com/qiniu/csharp-sdk
    /// </summary>
    public class Hashing
    {
        /// <summary>
        /// 计算SHA1
        /// </summary>
        /// <param name="data">字节数据</param>
        /// <returns>SHA1</returns>
        public static byte[] CalcSHA1(byte[] data)
        {
#if WINDOWS_UWP
            var sha = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha1);
            var buf = CryptographicBuffer.CreateFromByteArray(data);
            var digest = sha.HashData(buf);
            var hashBytes = new byte[digest.Length];
            CryptographicBuffer.CopyToByteArray(digest, out hashBytes);
            return hashBytes;
#else
            SHA1 sha1 = SHA1.Create();
            return sha1.ComputeHash(data);
#endif
        }

        /// <summary>
        /// 计算MD5哈希(可能需要关闭FIPS)
        /// </summary>
        /// <param name="str">待计算的字符串</param>
        /// <returns>MD5结果</returns>
        public static string CalcMD5(string str)
        {
#if WINDOWS_UWP
            var md5 = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buf = CryptographicBuffer.ConvertStringToBinary(str, BinaryStringEncoding.Utf8);
            var digest = md5.HashData(buf);
            return CryptographicBuffer.EncodeToHexString(digest);
#else
            MD5 md5 = MD5.Create();
            byte[] data = Encoding.UTF8.GetBytes(str);
            byte[] hashData = md5.ComputeHash(data);
            StringBuilder sb = new StringBuilder(hashData.Length * 2);
            foreach (byte b in hashData)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
#endif
        }

        /// <summary>
        /// 计算MD5哈希(第三方实现)
        /// </summary>
        /// <param name="str">待计算的字符串,避免FIPS-Exception</param>
        /// <returns>MD5结果</returns>
        public static string CalcMD5X(string str)
        {
            byte[] data = Encoding.UTF8.GetBytes(str);
            LabMD5 md5 = new LabMD5();
            return md5.ComputeHash(data);
        }

    }
}
