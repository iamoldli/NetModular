using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Blog.Domain.Article
{
    /// <summary>
    /// 文章
    /// </summary>
    [Table("Article")]
    public partial class ArticleEntity : EntityBase
    {
        /// <summary>
        /// 分类
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; } = DateTime.Now;

    }
}
