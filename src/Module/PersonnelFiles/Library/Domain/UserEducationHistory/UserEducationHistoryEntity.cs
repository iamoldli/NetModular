using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.PersonnelFiles.Domain.UserEducationHistory
{
    /// <summary>
    /// 用户教育经历
    /// </summary>
    [Table("User_Education_History")]
    public partial class UserEducationHistoryEntity : EntityBase<int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        [Length(100)]
        public string SchoolName { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        [Length(100)]
        public string Profession { get; set; }
    }
}
