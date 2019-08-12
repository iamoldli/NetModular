using System;

namespace Tm.Lib.Data.Core.Internal
{
    /// <summary>
    /// 
    /// </summary>
    internal class Check
    {
        /// <summary>
        /// 检测对象是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        public static void NotNull<T>(T obj, string parameterName, string message = null)
        {
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException(parameterName, message ?? $"{parameterName} is null");
        }

        /// <summary>
        /// 检测字符串是否为空
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        /// <param name="message"></param>
        public static void NotNull(string obj, string parameterName, string message = null)
        {
            if (string.IsNullOrWhiteSpace(obj))
                throw new ArgumentNullException(parameterName, message ?? $"{parameterName} is null");
        }
    }
}
