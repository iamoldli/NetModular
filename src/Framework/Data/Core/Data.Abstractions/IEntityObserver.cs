using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Abstractions
{
    /// <summary>
    /// 实体信息观察者接口
    /// </summary>
    public interface IEntityObserver<T> where T : IEntity
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id">新增实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Add(dynamic id, IUnitOfWork uow = null);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">修改实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Update(dynamic id, IUnitOfWork uow = null);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">删除实体ID</param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task Delete(dynamic id, IUnitOfWork uow = null);
    }
}
