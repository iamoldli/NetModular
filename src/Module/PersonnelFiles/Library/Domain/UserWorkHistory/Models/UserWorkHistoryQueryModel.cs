using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.PersonnelFiles.Domain.UserWorkHistory.Models
{
    public class UserWorkHistoryQueryModel : QueryModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

    }
}
