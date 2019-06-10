using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Common.Domain.Dict.Models;

namespace Nm.Module.Common.Domain.Dict
{
    /// <summary>
    /// 字典仓储
    /// </summary>
    public interface IDictRepository : IRepository<DictEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<DictEntity>> Query(DictQueryModel model);
    }
}
