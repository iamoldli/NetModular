using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.Tenant
{
    /// <summary>
    /// 租户仓储
    /// </summary>
    public interface ITenantRepository : IRepository<TenantEntity>
    {
    }
}
