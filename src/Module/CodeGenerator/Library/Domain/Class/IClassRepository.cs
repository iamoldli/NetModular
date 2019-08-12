using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.CodeGenerator.Domain.Class.Models;

namespace Tm.Module.CodeGenerator.Domain.Class
{
    /// <summary>
    /// 实体信息仓储
    /// </summary>
    public interface IClassRepository : IRepository<ClassEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ClassEntity>> Query(ClassQueryModel model);

        /// <summary>
        /// 查询指定项目的所有类列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task<IList<ClassEntity>> QueryAllByProject(Guid projectId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <returns></returns>
        Task<bool> Exists(ClassEntity entity);

        /// <summary>
        /// 删除制定项目的所有类
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction);
    }
}
