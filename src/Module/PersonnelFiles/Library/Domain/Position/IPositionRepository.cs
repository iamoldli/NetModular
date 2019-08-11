using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.PersonnelFiles.Domain.Position.Models;

namespace Nm.Module.PersonnelFiles.Domain.Position
{
    /// <summary>
    /// 岗位仓储
    /// </summary>
    public interface IPositionRepository : IRepository<PositionEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<PositionEntity>> Query(PositionQueryModel model);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(PositionEntity entity);

        /// <summary>
        /// 查询指定部门的岗位列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<IList<PositionEntity>> QueryByDepartment(Guid departmentId);
    }
}
