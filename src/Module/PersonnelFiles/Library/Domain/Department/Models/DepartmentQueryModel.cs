using System;
using System.ComponentModel.DataAnnotations;
using Tm.Lib.Data.Query;

namespace Tm.Module.PersonnelFiles.Domain.Department.Models
{
    public class DepartmentQueryModel : QueryModel
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        [Required(ErrorMessage = "请选择公司单位")]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
