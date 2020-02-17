using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions
{
    /// <summary>
    /// 实体观察者处理器
    /// </summary>
    public interface IEntityObserverHandler
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Add<T>(dynamic id, IUnitOfWork uow = null) where T : IEntity;

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Update<T>(dynamic id, IUnitOfWork uow = null) where T : IEntity;

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Delete<T>(dynamic id, IUnitOfWork uow = null) where T : IEntity;
    }
}
