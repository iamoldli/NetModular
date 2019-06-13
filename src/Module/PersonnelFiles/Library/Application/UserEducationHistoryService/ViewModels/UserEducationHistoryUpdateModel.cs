using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.PersonnelFiles.Domain.UserEducationHistory;

namespace Nm.Module.PersonnelFiles.Application.UserEducationHistoryService.ViewModels
{
    /// <summary>
    /// 用户教育经历添加模型
    /// </summary>
    public class UserEducationHistoryUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的用户教育经历")]
        public Guid Id { get; set; }

            /// <summary>
        /// 学历
        /// </summary>
        public string Level { get; set; }

            /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }

            /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

            /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

            /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

    
    }
}
