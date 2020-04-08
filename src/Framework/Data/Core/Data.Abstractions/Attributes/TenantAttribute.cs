using System;

namespace NetModular.Lib.Data.Abstractions.Attributes
{
    /// <summary>
    /// 多租户
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TenantAttribute : Attribute
    {
        /// <summary>
        /// 租户编号对应数据库字段名称
        /// </summary>
        public string TenantIdColumnName { get; set; }

        /// <summary>
        /// 租户编号对应数据库字段名称
        /// </summary>
        /// <param name="tenantIdColumnName"></param>
        public TenantAttribute(string tenantIdColumnName)
        {
            Check.NotNull(tenantIdColumnName, nameof(tenantIdColumnName), "租户ID的字段名称不能为空");
            TenantIdColumnName = tenantIdColumnName;
        }

        public TenantAttribute()
        {
            TenantIdColumnName = "TenantId";
        }
    }
}
