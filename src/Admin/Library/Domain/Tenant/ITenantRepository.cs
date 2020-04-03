using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Tenant.Models;

namespace NetModular.Module.Admin.Domain.Tenant
{
    /// <summary>
    /// 租户仓储
    /// </summary>
    public interface ITenantRepository : IRepository<TenantEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<TenantEntity>> Query(TenantQueryModel model);

        /// <summary>
        /// 根据租户名查询租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns></returns>
        Task<TenantEntity> GetById(string tenantName);

    }
}
