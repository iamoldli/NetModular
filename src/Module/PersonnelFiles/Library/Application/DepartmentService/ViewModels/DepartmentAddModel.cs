using System;

namespace Nm.Module.PersonnelFiles.Application.DepartmentService.ViewModels
{
    /// <summary>
    /// 部门添加模型
    /// </summary>
    public class DepartmentAddModel
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public Guid? Leader { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

    }
}
