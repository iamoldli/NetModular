using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.CodeGenerator.Application.ProjectService.ViewModels
{
    /// <summary>
    /// 项目编辑
    /// </summary>
    public class ProjectUpdateModel : ProjectAddModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Required(ErrorMessage = "请选择模块")]
        public Guid Id { get; set; }
    }
}
