using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.ModuleInfo.Models;

namespace NetModular.Module.Admin.Domain.ModuleInfo
{
    /// <summary>
    /// 模块仓储
    /// </summary>
    public interface IModuleInfoRepository : IRepository<ModuleInfoEntity>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        Task<IList<ModuleInfoEntity>> Query(ModuleInfoQueryModel model);

        /// <summary>
        /// 判断模块是否已存在
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> Exists(ModuleInfoEntity entity, IUnitOfWork uow);

        /// <summary>
        /// 根据编码更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateByCode(ModuleInfoEntity entity);
    }
}
