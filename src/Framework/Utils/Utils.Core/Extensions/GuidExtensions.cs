using System;

// ReSharper disable once CheckNamespace
namespace NetModular
{
    public static class GuidExtensions
    {
        /// <summary>
        /// 判断Guid是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this Guid s)
        {
            return s == Guid.Empty;
        }

        /// <summary>
        /// 判断Guid是否不为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool NotEmpty(this Guid s)
        {
            return s != Guid.Empty;
        }
    }
}
