using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.ModuleInfo.Models;

namespace Nm.Module.Admin.Domain.ModuleInfo
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
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> Exists(ModuleInfoEntity entity, IDbTransaction transaction);

        /// <summary>
        /// 根据编码更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateByCode(ModuleInfoEntity entity);
    }
}
