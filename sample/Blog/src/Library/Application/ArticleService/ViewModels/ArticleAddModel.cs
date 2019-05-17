using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Blog.Domain.Article;

namespace Nm.Module.Blog.Application.ArticleService.ViewModels
{
    /// <summary>
    /// 文章添加模型
    /// </summary>
    public class ArticleAddModel
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 标签列表
        /// </summary>
        public string TagList { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public Guid CategoryId { get; set; }

    }
}
