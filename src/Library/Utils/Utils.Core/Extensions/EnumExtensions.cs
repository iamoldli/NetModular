using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Utils.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举类型的Description说明
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            var info = type.GetField(value.ToString());
            return info.GetCustomAttributes(typeof(DescriptionAttribute), true)[0] is DescriptionAttribute
                descriptionAttribute
                ? descriptionAttribute.Description
                : value.ToString();
        }

        /// <summary>
        /// 枚举转换为返回模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ignoreUnKnown">忽略UnKnown选项</param>
        /// <returns></returns>
        public static List<OptionResultModel> ToResult<T>(bool ignoreUnKnown = false)
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                return null;

            return Enum.GetValues(enumType).Cast<Enum>()
                 .Where(m => !ignoreUnKnown || !m.ToString().Equals("UnKnown")).Select(x => new OptionResultModel
                 {
                     Label = x.ToDescription(),
                     Value = x
                 }).ToList();
        }
    }
}
