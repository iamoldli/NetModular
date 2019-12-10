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
        /// <returns></returns>
        Task Add<T>(dynamic id) where T : IEntity;

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Update<T>(dynamic id) where T : IEntity;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete<T>(dynamic id) where T : IEntity;
    }
}
