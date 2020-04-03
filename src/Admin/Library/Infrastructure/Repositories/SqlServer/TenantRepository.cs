using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Tenant;
using NetModular.Module.Admin.Domain.Tenant.Models;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class TenantRepository : RepositoryAbstract<TenantEntity>, ITenantRepository
    {
        public TenantRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<TenantEntity>> Query(TenantQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }
            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var result = await joinQuery.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        /// <summary>
        /// 根据租户名查询租户信息
        /// </summary>
        /// <param name="tenantName">租户名</param>
        /// <returns></returns>
        public Task<TenantEntity> GetById(string tenantName)
        {
            return Db.Find(m => m.Name == tenantName).FirstAsync();
        }



    }
}
