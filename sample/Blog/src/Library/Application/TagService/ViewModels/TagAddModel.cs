using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Blog.Domain.Tag;

namespace Nm.Module.Blog.Application.TagService.ViewModels
{
    /// <summary>
    /// 标签添加模型
    /// </summary>
    public class TagAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
