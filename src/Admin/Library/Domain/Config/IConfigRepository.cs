using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Config.Models;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项仓储
    /// </summary>
    public interface IConfigRepository : IRepository<ConfigEntity>
    {
        /// <summary>
        /// 验证键值是否存在
        /// </summary>
        /// <param name="type">配置类型</param>
        /// <param name="key">键名</param>
        /// <returns></returns>
        Task<bool> Exists(ConfigType type, string key);

        /// <summary>
        /// 数据项是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(ConfigEntity entity);

        /// <summary>
        /// 根据类型查询所有配置列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<IList<ConfigEntity>> QueryByType(ConfigType type);

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
        /// <param name="type">类型</param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<ConfigEntity> GetByKey(string key, ConfigType type = ConfigType.Custom, IUnitOfWork uow = null);

        /// <summary>
        /// 根据Key模糊查询
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<ConfigEntity> GetByKeyWithLike(string key, ConfigType type = ConfigType.Custom);
    }
}
