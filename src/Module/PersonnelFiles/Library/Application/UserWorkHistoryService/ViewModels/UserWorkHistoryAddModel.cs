using System;
using System.ComponentModel.DataAnnotations;

namespace Tm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels
{
    public class UserWorkHistoryAddModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required(ErrorMessage = "请选择用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Required(ErrorMessage = "请输入企业名称")]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        [Required(ErrorMessage = "请输入起始日期")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [Required(ErrorMessage = "请输入结束日期")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系人手机号
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        public string DimissionReason { get; set; }
    }
}
