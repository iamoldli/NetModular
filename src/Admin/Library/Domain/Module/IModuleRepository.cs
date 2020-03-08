using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.Module
{
    /// <summary>
    /// 模块仓储
    /// </summary>
    public interface IModuleRepository : IRepository<ModuleEntity>
    {
        /// <summary>
        /// 判断模块是否已存在
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> Exists(ModuleEntity entity, IUnitOfWork uow);

        /// <summary>
        /// 根据编码更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> UpdateByCode(ModuleEntity entity);

        /// <summary>
        /// 修改接口请求数量
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="count">数量</param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> UpdateApiRequestCount(string code, long count, IUnitOfWork uow = null);
    }
}
