using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.PersonnelFiles.Domain.Department.Models;

namespace Nm.Module.PersonnelFiles.Domain.Department
{
    /// <summary>
    /// 部门仓储
    /// </summary>
    public interface IDepartmentRepository : IRepository<DepartmentEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DepartmentEntity>> Query(DepartmentQueryModel model);

        /// <summary>
        /// 查询某公司下的所有部门列表
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<IList<DepartmentEntity>> QueryAllByCompany(Guid companyId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(DepartmentEntity entity);

        /// <summary>
        /// 判断是否存在子部门
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<bool> ExistsChildren(Guid parentId);
    }
}
