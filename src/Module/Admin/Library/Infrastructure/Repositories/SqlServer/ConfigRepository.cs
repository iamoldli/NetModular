using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.Config;

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

        public Task<IList<ConfigEntity>> QueryByPrefix(string prefix)
        {
            return Db.Find(m => m.Key.StartsWith(prefix)).ToListAsync();
        }

        
        public override async Task<bool> UpdateAsync(ConfigEntity entity)
        {
            if (await Exists(entity.Key))
                return await Db.Find(m => m.Key == entity.Key).UpdateAsync(m => new ConfigEntity { Value = entity.Value, Remarks = entity.Remarks });

            return await AddAsync(entity);
        }
    }
}
