using System.ComponentModel;

namespace Tm.Module.CodeGenerator.Domain.Property
{
    public enum PropertyType
    {
        [Description("string")]
        String,
        [Description("byte")]
        Byte,
        [Description("short")]
        Short,
        [Description("int")]
        Int,
        [Description("long")]
        Long,
        [Description("double")]
        Double,
        [Description("decimal")]
        Decimal,
        [Description("bool")]
        Bool,
        [Description("Guid")]
        Guid,
        [Description("DateTime")]
        DateTime,
        [Description("Enum")]
        Enum
    }
}
