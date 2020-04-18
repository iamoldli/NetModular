using System.ComponentModel;

namespace NetModular.Lib.Quartz.Abstractions
{
    public enum QuartzSerializerType
    {
        [Description("JSON")]
        Json,
        [Description("XML")]
        Xml
    }
}
