using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities;

namespace Nm.Module.PersonnelFiles.Domain.UserContact
{
    /// <summary>
    /// 用户联系信息
    /// </summary>
    [Table("User_Contact")]
    public partial class UserContactEntity : Entity<int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [Length(20)]
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }

        /// <summary>
        /// 钉钉
        /// </summary>
        public string DingDing { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        [Length(12)]
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        [Length(12)]
        public string CityCode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区县编码
        /// </summary>
        [Length(12)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 城镇编码
        /// </summary>
        [Length(12)]
        public string TownCode { get; set; }

        /// <summary>
        /// 城镇名称
        /// </summary>
        [Length(100)]
        public string TownName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Length(200)]
        public string Address { get; set; }
    }
}
