using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace NetModular
{
    public static class EnumExtensions
    {
        private static readonly ConcurrentDictionary<string, string> DescriptionCache = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 包含UnKnown选项
        /// </summary>
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, List<OptionResultModel>> ListCache = new ConcurrentDictionary<RuntimeTypeHandle, List<OptionResultModel>>();

        /// <summary>
        /// 不包含UnKnown选项
        /// </summary>
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, List<OptionResultModel>> ListCacheNoIgnore = new ConcurrentDictionary<RuntimeTypeHandle, List<OptionResultModel>>();

        /// <summary>
        /// 获取枚举类型的Description说明
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            var info = type.GetField(value.ToString());
            var key = type.FullName + info.Name;
            if (!DescriptionCache.TryGetValue(key, out string desc))
            {
                var attrs = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs.Length < 1)
                    desc = string.Empty;
                else
                    desc = attrs[0] is DescriptionAttribute
                        descriptionAttribute
                        ? descriptionAttribute.Description
                        : value.ToString();

                DescriptionCache.TryAdd(key, desc);
            }

            return desc;
        }

        public static List<OptionResultModel> ToResult(this Enum value, bool ignoreUnKnown = false)
        {
            var enumType = value.GetType();

            if (!enumType.IsEnum)
                return null;

            return Enum.GetValues(enumType).Cast<Enum>()
                .Where(m => !ignoreUnKnown || !m.ToString().Equals("UnKnown")).Select(x => new OptionResultModel
                {
                    Label = x.ToDescription(),
                    Value = x.ToInt()
                }).ToList();
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

            if (ignoreUnKnown)
            {
                #region ==忽略UnKnown属性==

                if (!ListCacheNoIgnore.TryGetValue(enumType.TypeHandle, out List<OptionResultModel> list))
                {
                    list = Enum.GetValues(enumType).Cast<Enum>()
                        .Where(m => !m.ToString().Equals("UnKnown")).Select(x => new OptionResultModel
                        {
                            Label = x.ToDescription(),
                            Value = x.ToInt()
                        }).ToList();

                    ListCacheNoIgnore.TryAdd(enumType.TypeHandle, list);
                }

                return list.Select(m => new OptionResultModel { Label = m.Label, Value = m.Value }).ToList();

                #endregion ==忽略UnKnown属性==
            }
            else
            {
                #region ==包含UnKnown选项==

                if (!ListCache.TryGetValue(enumType.TypeHandle, out List<OptionResultModel> list))
                {
                    list = Enum.GetValues(enumType).Cast<Enum>().Select(x => new OptionResultModel
                    {
                        Label = x.ToDescription(),
                        Value = x.ToInt()
                    }).ToList();

                    ListCache.TryAdd(enumType.TypeHandle, list);
                }

                return list.Select(m => new OptionResultModel { Label = m.Label, Value = m.Value }).ToList();

                #endregion ==包含UnKnown选项==
            }
        }
    }
}