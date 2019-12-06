using System;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Utils.Core.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 日期格式化
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="format">默认：yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static string Format(this DateTime dateTime, string format = null)
        {
            if (format.IsNull())
                format = "yyyy-MM-dd HH:mm:ss";

            return dateTime.ToString(format);
        }

        /// <summary>
        /// 转换为时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="milliseconds">是否使用毫秒</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dateTime, bool milliseconds = false)
        {
            var ts = dateTime.ToUniversalTime() - DateTimeHelper.TimestampStart;
            return (milliseconds ? ts.TotalMilliseconds : ts.TotalSeconds).ToLong();
        }

        /// <summary>
        /// 获取周几
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string GetWeek(this DateTime datetime)
        {
            var dayOfWeek = datetime.DayOfWeek.ToInt();
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
