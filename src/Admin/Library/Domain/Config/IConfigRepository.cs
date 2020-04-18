using System.Threading.Tasks;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public interface IConfigRepository : IRepository<ConfigEntity>
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="type"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<ConfigEntity> Get(ConfigType type, string code);
    }
}
