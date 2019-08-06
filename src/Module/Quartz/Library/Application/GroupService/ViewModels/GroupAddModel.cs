using System.ComponentModel.DataAnnotations;

namespace Nm.Module.Quartz.Application.GroupService.ViewModels
{
    /// <summary>
    /// 任务组添加模型
    /// </summary>
    public class GroupAddModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入组名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required(ErrorMessage = "请输入编码")]
        public string Code { get; set; }
    }
}
