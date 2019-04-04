using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Role;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleRepository : RepositoryAbstract<Role>, IRoleRepository
    {
        public RoleRepository(IDbContext dbContext) : base(dbContext)
        {

        }

        public Task<bool> Exists(string name, Guid? id = null)
        {
            var query = Db.Find(m => m.Deleted == false && m.Name.Equals(name));
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<IList<Role>> Query(Paging paging, string name = null)
        {
            var query = Db.Find(m => m.Deleted == false).LeftJoin<Account>((x, y) => x.CreatedBy == y.Id);
            query.WhereIf(name.NotNull(), (x, y) => x.Name.Contains(name));
            query.Select((x, y) => new { x, Creator = y.Name });
            return query.PaginationAsync(paging);
        }

        public override Task<IList<Role>> GetAllAsync()
        {
            return Db.Find(m => m.Deleted == false).ToListAsync();
        }
    }
}
