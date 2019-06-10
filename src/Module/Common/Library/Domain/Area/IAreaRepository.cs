using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Common.Domain.Area.Models;

namespace Nm.Module.Common.Domain.Area
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
    }
}
