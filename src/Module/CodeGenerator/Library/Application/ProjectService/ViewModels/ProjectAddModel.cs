using System.ComponentModel.DataAnnotations;

namespace Nm.Module.CodeGenerator.Application.ProjectService.ViewModels
{
    /// <summary>
    /// 项目添加
    /// </summary>
    public class ProjectAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入项目名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "请输入项目编码")]
        public string Code { get; set; }
    }
}
