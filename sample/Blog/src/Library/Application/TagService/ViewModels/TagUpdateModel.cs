using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.Blog.Domain.Tag;

namespace Nm.Module.Blog.Application.TagService.ViewModels
{
    /// <summary>
    /// 标签添加模型
    /// </summary>
    public class TagUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的标签")]
        public Guid Id { get; set; }

            /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    
    }
}
