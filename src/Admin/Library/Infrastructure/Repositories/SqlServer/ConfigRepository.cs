using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Domain.Config.Models;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ConfigRepository : RepositoryAbstract<ConfigEntity>, IConfigRepository
    {
        public ConfigRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exists(ConfigType type, string key)
        {
            return ExistsAsync(m => m.Type == type && m.Key == key);
        }

        public Task<bool> Exists(ConfigEntity entity)
        {
            var query = Db.Find(m => m.Key == entity.Key && m.Type == entity.Type);
            query.WhereIf(entity.Id > 0, m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<IList<ConfigEntity>> QueryByType(ConfigType type)
        {
            return Db.Find(m => m.Type == type).ToListAsync();
        }

        public async Task<IList<ConfigEntity>> Query(ConfigQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Key, m => m.Key.Contains(model.Key) || m.Value.Contains(model.Key));
            query.WhereNotNull(model.Type, m => m.Type == model.Type.Value);

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

        public Task<ConfigEntity> GetByKey(string key, ConfigType type = ConfigType.Custom, IUnitOfWork uow = null)
        {
            return Db.Find(m => m.Key == key && m.Type == type).UseUow(uow).FirstAsync();
        }

        public Task<ConfigEntity> GetByKeyWithLike(string key, ConfigType type = ConfigType.Custom)
        {
            return Db.Find(m => m.Key.Contains(key) && m.Type == type).FirstAsync();
        }

        public override async Task<bool> UpdateAsync(ConfigEntity entity, IUnitOfWork uow)
        {
            if (await ExistsAsync(m => m.Key.Equals(entity.Key), uow))
                return await Db.Find(m => m.Key == entity.Key).UseUow(uow).UpdateAsync(m => new ConfigEntity { Value = entity.Value, Remarks = entity.Remarks });

            return await AddAsync(entity, uow);
        }
    }
}
