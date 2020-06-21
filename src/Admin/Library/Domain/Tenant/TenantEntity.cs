using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Admin.Domain.Tenant
{
    /// <summary>
    /// 租户信息
    /// </summary>
    public class TenantEntity : EntityBase
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }
    }
}
