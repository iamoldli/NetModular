using System.ComponentModel;

namespace NetModular.Lib.Config.Abstraction
{
    /// <summary>
    /// 配置属性类型
    /// </summary>
    public enum ConfigPropertyType
    {
        [Description("String")]
        String,
        [Description("Int")]
        Int,
        [Description("Double")]
        Double,
        [Description("Decimal")]
        Decimal,
        [Description("Datetime")]
        DateTime
    }
}
