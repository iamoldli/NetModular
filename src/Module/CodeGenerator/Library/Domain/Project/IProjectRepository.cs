using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.CodeGenerator.Domain.Project.Models;

namespace Nm.Module.CodeGenerator.Domain.Project
{
    /// <summary>
    /// 项目仓储
    /// </summary>
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ProjectEntity>> Query(ProjectQueryModel model);
    }
}
