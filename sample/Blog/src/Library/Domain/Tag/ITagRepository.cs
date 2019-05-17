using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Blog.Domain.Tag.Models;

namespace Nm.Module.Blog.Domain.Tag
{
    /// <summary>
    /// 标签仓储
    /// </summary>
    public interface ITagRepository : IRepository<TagEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<TagEntity>> Query(TagQueryModel model);
    }
}
