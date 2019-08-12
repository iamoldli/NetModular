using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Admin.Domain.Role;
using Tm.Module.Admin.Domain.Role.Models;

namespace Tm.Module.Admin.Infrastructure.Repositories.SqlServer
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
            var query = Db.Find(m => m.Deleted == false);
            query.WhereIf(!model.CID.IsEmpty(), m => m.CID==model.CID);
            var query1=query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            query1.Select((x, y) => new { x, Creator = y.Name });
            var list = await query1.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public override Task<IList<RoleEntity>> GetAllAsync()
        {
            return Db.Find(m => m.Deleted == false).ToListAsync();
        }
    }
}
