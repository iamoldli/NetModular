using System;

namespace NetModular.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 表名称，指定实体类在数据库中对应的表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指定实体类在数据库中对应的表名称
        /// </summary>
        /// <param name="tableName">表名</param>
        public TableAttribute(string tableName)
        {
            Name = tableName;
        }
    }
}
