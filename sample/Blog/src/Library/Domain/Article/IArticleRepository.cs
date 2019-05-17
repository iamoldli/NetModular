using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.Blog.Domain.Article.Models;

namespace Nm.Module.Blog.Domain.Article
{
    /// <summary>
    /// 文章仓储
    /// </summary>
    public interface IArticleRepository : IRepository<ArticleEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ArticleEntity>> Query(ArticleQueryModel model);
    }
}
