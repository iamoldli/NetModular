using System;

namespace Nm.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 忽略属性，不创建实体对应的表或者实体属性不在表中映射列
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, Inherited = false)]
    public class IgnoreAttribute : Attribute
    {
    }
}