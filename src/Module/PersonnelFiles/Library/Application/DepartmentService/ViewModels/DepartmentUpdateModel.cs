using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.PersonnelFiles.Application.DepartmentService.ViewModels
{
    /// <summary>
    /// 部门添加模型
    /// </summary>
    public class DepartmentUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的部门")]
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public Guid Leader { get; set; }

        /// <summary>
        /// 负责人名称
        /// </summary>
        public string LeaderName { get; set; }
    }
}
