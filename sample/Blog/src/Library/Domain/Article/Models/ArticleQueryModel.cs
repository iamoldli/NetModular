using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.Blog.Domain.Article.Models
{
    public class ArticleQueryModel : QueryModel
    {
        /// <summary>
        /// 分类
        /// </summary>
        public Guid? CategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

    }
}
