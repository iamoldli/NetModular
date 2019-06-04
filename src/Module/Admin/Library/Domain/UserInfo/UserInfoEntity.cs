using System;
using Nm.Lib.Data.Core.Entities.Extend;
using Nm.Lib.Utils.Core.Enums;

namespace Nm.Module.Admin.Domain.UserInfo
{
    /// <summary>
    /// 用户基础信息
    /// </summary>
    public class UserInfoEntity : EntityBaseWithSoftDelete<int, Guid>
    {
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
        public string Native { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus  { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public string Picture { get; set; }
    }
}
