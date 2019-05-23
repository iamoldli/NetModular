using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Blog.Domain.Category.Models;

namespace Nm.Module.Blog.Domain.Category
{
    /// <summary>
    /// 分类仓储
    /// </summary>
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<CategoryEntity>> Query(CategoryQueryModel model);
    }
}
