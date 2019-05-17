using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.CodeGenerator.Application.ProjectService.ViewModels
{
    /// <summary>
    /// 生成代码模型
    /// </summary>
    public class ProjectBuildCodeModel
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Required(ErrorMessage = "请选择项目")]
        public Guid Id { get; set; }
    }
}
