using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public interface IConfigRepository : IRepository<Config>
    {
        /// <summary>
        /// 验证键值是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> Exists(string key);

        /// <summary>
        /// 根据前缀查询配置项列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<IList<Config>> QueryByPrefix(string prefix);
    }
}
