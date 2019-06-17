using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.PersonnelFiles.Domain.UserEducationHistory.Models
{
    public class UserEducationHistoryQueryModel : QueryModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

    }
}
