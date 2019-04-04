using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ConfigRepository : RepositoryAbstract<Config>, IConfigRepository
    {
        public ConfigRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exists(string key)
        {
            return ExistsAsync(m => m.Key.Equals(key));
        }

        public Task<IList<Config>> QueryByPrefix(string prefix)
        {
            return Db.Find(m => m.Key.StartsWith(prefix)).ToListAsync();
        }

        
        public override async Task<bool> UpdateAsync(Config entity)
        {
            if (await Exists(entity.Key))
                return await Db.Find(m => m.Key == entity.Key).UpdateAsync(m => new Config { Value = entity.Value, Remarks = entity.Remarks });

            return await AddAsync(entity);
        }
    }
}
