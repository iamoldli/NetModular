using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.PersonnelFiles.Domain.Position;

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
        /// 部门编码
        /// </summary>
        public Guid DepartmentId { get; set; }

            /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    
    }
}
