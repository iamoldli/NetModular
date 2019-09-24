using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.PersonnelFiles.Domain.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Table("User")]
    public partial class UserEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 关联账户编号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public int JobNo { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 职位编号
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        [Nullable]
        public string Native { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        [Nullable]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [Nullable]
        public string Nation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [Nullable]
        public string Education { get; set; }

        /// <summary>
        /// 照片关联的附件编号
        /// </summary>
        [Nullable]
        public Guid Picture { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Nullable]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Length(250)]
        [Nullable]
        public string Email { get; set; }
    }
}
