using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.CodeGenerator.Domain.Project.Models;

namespace Tm.Module.CodeGenerator.Domain.Project
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
