using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.PersonnelFiles.Domain.UserWorkHistory
{
    /// <summary>
    /// 用户工作经历
    /// </summary>
    [Table("User_Work_History")]
    public partial class UserWorkHistoryEntity : EntityBase<int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Length(100)]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
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
        [Length(300)]
        public string DimissionReason { get; set; }
    }
}
