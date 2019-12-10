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
        /// <returns></returns>
        Task Add(dynamic id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Update(dynamic id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(dynamic id);
    }
}
