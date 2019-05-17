using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.Role;
using Nm.Module.Admin.Domain.Role.Models;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class RoleRepository : RepositoryAbstract<RoleEntity>, IRoleRepository
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

        public async Task<IList<RoleEntity>> Query(RoleQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find(m => m.Deleted == false).LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            query.WhereIf(model.Name.NotNull(), (x, y) => x.Name.Contains(model.Name));
            query.Select((x, y) => new { x, Creator = y.Name });
            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public override Task<IList<RoleEntity>> GetAllAsync()
        {
            return Db.Find(m => m.Deleted == false).ToListAsync();
        }
    }
}
