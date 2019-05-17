using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Blog.Domain.Category
{
    /// <summary>
    /// 分类
    /// </summary>
    [Table("Category")]
    public partial class CategoryEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
