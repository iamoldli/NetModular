using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Admin.Domain.Tenant
{
    /// <summary>
    /// 租户信息
    /// </summary>
    [Table("Tenant")]
    public class TenantEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
