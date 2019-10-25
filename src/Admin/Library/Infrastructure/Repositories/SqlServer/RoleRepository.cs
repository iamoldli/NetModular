using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Domain.Role.Models;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleRepository : RepositoryAbstract<RoleEntity>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> Exists(string name, Guid? id = null)
        {
            var query = Db.Find(m => m.Name.Equals(name));
            query.WhereNotNull(id, m => m.Id != id);
            return query.ExistsAsync();
        }

        public async Task<IList<RoleEntity>> Query(RoleQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }
    }
}
