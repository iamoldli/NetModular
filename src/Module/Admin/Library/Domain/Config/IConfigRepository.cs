using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public interface IConfigRepository : IRepository<ConfigEntity>
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
        Task<IList<ConfigEntity>> QueryByPrefix(string prefix);
    }
}
