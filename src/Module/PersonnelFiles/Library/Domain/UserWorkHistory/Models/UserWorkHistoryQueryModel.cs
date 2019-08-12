using System;
using System.ComponentModel.DataAnnotations;
using Tm.Lib.Data.Query;

namespace Tm.Module.PersonnelFiles.Domain.UserWorkHistory.Models
{
    public class UserWorkHistoryQueryModel : QueryModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required(ErrorMessage = "请选择用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; }
    }
}
