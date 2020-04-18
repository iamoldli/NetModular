using System.Threading.Tasks;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class ConfigRepository : RepositoryAbstract<ConfigEntity>, IConfigRepository
    {
        public ConfigRepository(IDbContext context) : base(context)
        {
        }

        public Task<ConfigEntity> Get(ConfigType type, string code)
        {
            return GetAsync(m => m.Type == type && m.Code == code);
        }
    }
}
