using System;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core.Helpers
{
    [Singleton]
    public class DateTimeHelper
    {
        /// <summary>
        /// 时间戳起始日期
        /// </summary>
        public static DateTime TimeStampStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public string GetTimeStamp(bool milliseconds = false)
        {
            var ts = DateTime.UtcNow - TimeStampStart;
            return Convert.ToInt64(milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds).ToString();
        }
    }
}
