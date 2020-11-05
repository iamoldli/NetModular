using System;
using System.ComponentModel;
using NetModular.Lib.Utils.Core.Attributes;

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

        /// <summary>
        /// 时间戳转日期
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public DateTime TimeStamp2DateTime(long timestamp, bool milliseconds = false)
        {
            var value = milliseconds ? 10000 : 10000000;
            return TimestampStart.AddTicks(timestamp * value);
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

        #region 获取 本周、本月、本季度、本年 的开始时间或结束时间

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static DateTime? GetStart(StartAndEndDateType type, DateTime? date = null)
        {
            var d = date ?? DateTime.Now;

            switch (type)
            {
                case StartAndEndDateType.Week:
                    return d.AddDays(-(int)d.DayOfWeek + 1);
                case StartAndEndDateType.Month:
                    return d.AddDays(-d.Day + 1);
                case StartAndEndDateType.Season:
                    var time = d.AddMonths(0 - ((d.Month - 1) % 3));
                    return time.AddDays(-time.Day + 1);
                case StartAndEndDateType.Year:
                    return d.AddDays(-d.DayOfYear + 1);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取结束时间
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="date">日期</param>
        /// <returns></returns>
        public static DateTime? GetEnd(StartAndEndDateType type, DateTime? date = null)
        {
            var d = date ?? DateTime.Now;
            switch (type)
            {
                case StartAndEndDateType.Week:
                    return d.AddDays(7 - (int)d.DayOfWeek);
                case StartAndEndDateType.Month:
                    return d.AddMonths(1).AddDays(-d.AddMonths(1).Day + 1).AddDays(-1);
                case StartAndEndDateType.Season:
                    var time = d.AddMonths((3 - ((d.Month - 1) % 3) - 1));
                    return time.AddMonths(1).AddDays(-time.AddMonths(1).Day + 1).AddDays(-1);
                case StartAndEndDateType.Year:
                    var time2 = d.AddYears(1);
                    return time2.AddDays(-time2.DayOfYear);
                default:
                    return null;
            }
        }

        #endregion
    }

    /// <summary>
    /// 开始结束日期类型
    /// </summary>
    public enum StartAndEndDateType
    {
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month,
        /// <summary>
        /// 季度
        /// </summary>
        [Description("季度")]
        Season,
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year
    }
}
