using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Department.Models;

namespace Nm.Module.Admin.Domain.Department
{
    /// <summary>
    /// 部门仓储
    /// </summary>
    public interface IDepartmentRepository : IRepository<DepartmentEntity>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DepartmentEntity>> Query(DepartmentQueryModel model);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(DepartmentEntity entity);

        /// <summary>
        /// 是否包含子节点
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsChild(Guid id);

    }
}
