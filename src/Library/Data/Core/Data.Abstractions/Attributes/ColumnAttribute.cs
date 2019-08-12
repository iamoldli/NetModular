using System;

namespace Tm.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 列名，指定实体属性在表中对应的列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">列名</param>
        public ColumnAttribute(string columnName)
        {
            Name = columnName;
        }
    }
}
