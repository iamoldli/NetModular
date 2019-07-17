using System;
using Nm.Lib.Utils.Core.Attributes;

namespace Nm.Lib.Utils.Core.Helpers
{
    [Singleton]
    public class DateTimeHelper
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public string GetTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
