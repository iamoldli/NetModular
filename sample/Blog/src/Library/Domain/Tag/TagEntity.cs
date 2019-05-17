using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Blog.Domain.Tag
{
    /// <summary>
    /// 标签
    /// </summary>
    [Table("Tag")]
    public partial class TagEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
