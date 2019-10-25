using System;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Core.Helpers
{
    [Singleton]
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public string GetTimeStamp(bool milliseconds = false)
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds).ToString();
        }
    }
}
