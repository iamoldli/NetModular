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
        /// 是否多租户
        /// </summary>
        public bool IsTenant { get; set; }

        /// <summary>
        /// 租户编号对应数据库列名称
        /// </summary>
        public string TenantIdColumnName { get; set; }

        /// <summary>
        /// 指定实体类在数据库中对应的表名称
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="isTenant">是否多租户</param>
        /// <param name="tenantIdColumnName">租户编号对应数据库列名称</param>
        public TableAttribute(string tableName, bool isTenant = true, string tenantIdColumnName = "TenantId")
        {
            Name = tableName;
            IsTenant = isTenant;
            TenantIdColumnName = tenantIdColumnName.NotNull() ? tenantIdColumnName : "TenantId";
        }
    }
}
