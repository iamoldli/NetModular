using System;

namespace Nm.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 忽略表前缀，如果设置了表前缀，附加了该特性的表不会设置表前缀
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PrefixIgnoreAttribute : Attribute
    {
    }
}