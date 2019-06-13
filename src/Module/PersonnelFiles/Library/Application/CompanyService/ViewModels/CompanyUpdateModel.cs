using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.PersonnelFiles.Domain.Company;

namespace Nm.Module.PersonnelFiles.Application.CompanyService.ViewModels
{
    /// <summary>
    /// 公司单位添加模型
    /// </summary>
    public class CompanyUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的公司单位")]
        public Guid Id { get; set; }

            /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

            /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

            /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

            /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

            /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

    
    }
}
