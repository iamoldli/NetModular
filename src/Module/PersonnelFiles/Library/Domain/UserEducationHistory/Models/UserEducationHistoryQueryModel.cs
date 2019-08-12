using System;
using System.ComponentModel.DataAnnotations;
using  Tm.Lib.Data.Query;

namespace  Tm.Module.PersonnelFiles.Domain.UserEducationHistory.Models
{
    public class UserEducationHistoryQueryModel : QueryModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required(ErrorMessage = "请选择用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { get; set; }
    }
}
