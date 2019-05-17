using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Blog.Domain.Category;

namespace Nm.Module.Blog.Application.CategoryService.ViewModels
{
    /// <summary>
    /// 分类添加模型
    /// </summary>
    public class CategoryAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
