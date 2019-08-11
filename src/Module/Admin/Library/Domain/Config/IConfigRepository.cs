using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Config.Models;

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
        /// 数据项是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(ConfigEntity entity);

        /// <summary>
        /// 根据前缀查询配置项列表
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<IList<ConfigEntity>> QueryByPrefix(string prefix);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ConfigEntity>> Query(ConfigQueryModel model);

        /// <summary>
        /// 根据Key获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ConfigEntity> GetByKey(string key);
    }
}
