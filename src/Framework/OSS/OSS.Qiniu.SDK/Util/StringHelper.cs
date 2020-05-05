using System;
using System.Collections.Generic;
using System.Text;

namespace Qiniu.Util
{
    /// <summary>
    /// 字符串处理工具
    /// </summary>
    public class StringHelper
    {
        

        /// <summary>
        /// URL编码
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <returns>URL编码字符串</returns>
        public static string UrlEncode(string text)
        {
            return Uri.EscapeDataString(text);
        }

        /// <summary>
        /// URL键值对编码
        /// </summary>
        /// <param name="values">键值对</param>
        /// <returns>URL编码的键值对数据</returns>
        public static string UrlFormEncode(Dictionary<string, string> values)
        {
            StringBuilder urlValuesBuilder = new StringBuilder();
           
            foreach (KeyValuePair<string, string> kvp in values)
            {
                urlValuesBuilder.AppendFormat("{0}={1}&", Uri.EscapeDataString(kvp.Key), Uri.EscapeDataString(kvp.Value));
            }
            string encodedStr=urlValuesBuilder.ToString();
            return encodedStr.Substring(0, encodedStr.Length - 1);
        }

    }
}