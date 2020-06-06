using System;

namespace NetModular.Lib.Data.Abstractions.Attributes
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
        /// 列类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">列名</param>
        public ColumnAttribute(string columnName)
        {
            Name = columnName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="typeName">列类型名称</param>
        public ColumnAttribute(string columnName, string typeName)
        {
            Name = columnName;
            TypeName = typeName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="typeName">列类型名称</param>
        /// <param name="defaultValue">默认值</param>
        public ColumnAttribute(string columnName, string typeName, string defaultValue)
        {
            Name = columnName;
            TypeName = typeName;
            DefaultValue = defaultValue;
        }
    }
}
