using System;

namespace NetModular.Lib.Utils.Core.Attributes
{
    /// <summary>
    /// 单例注入(使用该特性的服务系统会自动注入)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class ScopedAttribute : Attribute
    {
    }
}
