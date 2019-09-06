using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
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
            var status = new List<ConfigStatus>
            {
                ConfigStatus.Add,
                ConfigStatus.Delete,
                ConfigStatus.Modified
            };

            return Db.Find(m => status.Contains(m.Status)).ToListAsync();
        }

        public async Task<IList<ConfigEntity>> Query(ConfigQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Key, m => m.Key.Contains(model.Key) || m.Value.Contains(model.Key));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);
            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var list = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<ConfigEntity> GetByKey(string key)
        {
            return Db.Find(m => m.Key == key).FirstAsync();
        }

        public override async Task<bool> UpdateAsync(ConfigEntity entity, IDbTransaction transaction)
        {
            if (await ExistsAsync(m => m.Key.Equals(entity.Key), transaction))
                return await Db.Find(m => m.Key == entity.Key).UseTran(transaction).UpdateAsync(m => new ConfigEntity { Value = entity.Value, Remarks = entity.Remarks });

            return await AddAsync(entity, transaction);
        }
    }
}
