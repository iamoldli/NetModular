using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
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
    }
}
