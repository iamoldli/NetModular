using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Blog.Domain.Category;

namespace Nm.Module.Blog.Application.CategoryService.ViewModels
{
    /// <summary>
    /// 分类添加模型
    /// </summary>
    public class CategoryUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的分类")]
        public Guid Id { get; set; }

            /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    
    }
}
