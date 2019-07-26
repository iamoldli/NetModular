using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.Config;
using Nm.Module.Admin.Domain.Config.Models;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ConfigRepository : RepositoryAbstract<ConfigEntity>, IConfigRepository
    {
        public ConfigRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exists(string key)
        {
            return ExistsAsync(m => m.Key.Equals(key));
        }

        public Task<bool> Exists(ConfigEntity entity)
        {
            var query = Db.Find(m => m.Key == entity.Key);
            query.WhereIf(entity.Id > 0, m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<IList<ConfigEntity>> QueryByPrefix(string prefix)
        {
            return Db.Find(m => m.Key.StartsWith(prefix)).ToListAsync();
        }

        public async Task<IList<ConfigEntity>> Query(ConfigQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereIf(model.Key.NotNull(), m => m.Key.Contains(model.Key) || m.Value.Contains(model.Key));
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id).Select((x, y) => new { x, Creator = y.Name });
            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<ConfigEntity> GetByKey(string key)
        {
            return Db.Find(m => m.Key == key).FirstAsync();
        }

        public override async Task<bool> UpdateAsync(ConfigEntity entity, IDbTransaction transacition)
        {
            if (await Exists(entity.Key))
                return await Db.Find(m => m.Key == entity.Key).UpdateAsync(m => new ConfigEntity { Value = entity.Value, Remarks = entity.Remarks });

            return await AddAsync(entity);
        }
    }
}
