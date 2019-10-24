using System;

namespace Nm.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 最大长度 SqlServer:navarchar(max) MySql:text
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxAttribute : Attribute
    {
    }
}
