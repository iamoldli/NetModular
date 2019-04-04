using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.ModuleInfo;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ModuleInfoRepository : RepositoryAbstract<ModuleInfo>, IModuleInfoRepository
    {
        public ModuleInfoRepository(IDbContext context) : base(context)
        {
        }

        public Task<IList<ModuleInfo>> Query(Paging paging, string name = null, string code = null)
        {
            var query = Db.Find();
            query.WhereIf(name.NotNull(), m => m.Name.Contains(name));
            query.WhereIf(code.NotNull(), m => m.Code.Contains(code));

            if (!paging.OrderBy.Any())
                query.OrderByDescending(m => m.Id);

            return query.LeftJoin<Account>((x, y) => x.CreatedBy == y.Id).Select((x, y) => new { x, Creator = y.Name }).PaginationAsync(paging);
        }

        public Task<bool> Exists(string code, Guid? id = null)
        {
            var query = Db.Find(m => m.Code == code);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }
    }
}
