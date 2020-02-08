namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 模块配置项属性类型
    /// </summary>
    public enum ModuleOptionPropertyType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 整数
        /// </summary>
        Int,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal,
        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime,
        /// <summary>
        /// 日期
        /// </summary>
        Date,
        /// <summary>
        /// 时间
        /// </summary>
        Time,
        /// <summary>
        /// 枚举
        /// </summary>
        Enum,
        /// <summary>
        /// 布尔值
        /// </summary>
        Bool
    }
}
