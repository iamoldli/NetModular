using System;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Lib.Utils.Core.Helpers
{
    [Singleton]
    public class DateTimeHelper
    {
        /// <summary>
        /// 时间戳起始日期
        /// </summary>
        public static DateTime TimestampStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public string GetTimeStamp(bool milliseconds = false)
        {
            var ts = DateTime.UtcNow - TimestampStart;
            return Convert.ToInt64(milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds).ToString();
        }

        /// <summary>判断当前年份是否是闰年</summary>
        /// <param name="year">年份</param>
        /// <returns></returns>
        private bool IsLeapYear(int year)
        {
            int n = year;
            if ((n % 400 == 0) || (n % 4 == 0 && n % 100 != 0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取周几
        /// </summary>
        /// <returns></returns>
        public string GetWeek()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek.ToInt();
            string week;
            switch (dayOfWeek)
            {
                case 0:
                    week = "星期日";
                    break;
                case 1:
                    week = "星期一";
                    break;
                case 2:
                    week = "星期二";
                    break;
                case 3:
                    week = "星期三";
                    break;
                case 4:
                    week = "星期四";
                    break;
                case 5:
                    week = "星期五";
                    break;
                default:
                    week = "星期六";
                    break;
            }
            return week;
        }
    }
}
