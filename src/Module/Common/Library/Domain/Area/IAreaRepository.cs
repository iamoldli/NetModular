using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.Common.Domain.Area.Models;

namespace Tm.Module.Common.Domain.Area
{
    /// <summary>
    /// 区划代码仓储
    /// </summary>
    public interface IAreaRepository : IRepository<AreaEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<AreaEntity>> Query(AreaQueryModel model);

        /// <summary>
        /// 查询所有子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<IList<AreaEntity>> QueryChildren(int parentId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(AreaEntity entity);

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AreaEntity> GetByCode(string code);
    }
}
