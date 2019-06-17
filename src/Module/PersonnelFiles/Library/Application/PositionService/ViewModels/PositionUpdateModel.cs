using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.PersonnelFiles.Application.PositionService.ViewModels
{
    /// <summary>
    /// 岗位添加模型
    /// </summary>
    public class PositionUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的岗位")]
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "请输入岗位名称")]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
    }
}
