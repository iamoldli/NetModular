using System;
using System.Linq;

namespace Nm.Lib.Utils.Core.Extensions
{
    /// <summary>
    /// 数组扩展
    /// </summary>
    public static class ArraryExtensions
    {
        /// <summary>
        /// 随机获取数组中的一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T RandomGet<T>(this T[] arr)
        {
            if (arr == null || !arr.Any())
                return default(T);

            var r = new Random();

            return arr[r.Next(arr.Length)];
        }
    }
}
