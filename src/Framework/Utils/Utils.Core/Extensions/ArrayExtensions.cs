using System;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace NetModular
{
    /// <summary>
    /// 数组扩展
    /// </summary>
    public static class ArrayExtensions
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
