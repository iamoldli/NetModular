using System;
using System.ComponentModel.DataAnnotations;
using Nm.Module.PersonnelFiles.Domain.UserContact;

namespace Nm.Module.PersonnelFiles.Application.UserContactService.ViewModels
{
    /// <summary>
    /// 用户联系信息添加模型
    /// </summary>
    public class UserContactUpdateModel
    {
        [Required(ErrorMessage = "请选择要修改的用户联系信息")]
        public Guid Id { get; set; }

            /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

            /// <summary>
        /// 钉钉
        /// </summary>
        public string DingDing { get; set; }

            /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

            /// <summary>
        /// 城市编码
        /// </summary>
        public string CityCode { get; set; }

            /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

            /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

            /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserId { get; set; }

            /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }

            /// <summary>
        /// 区县编码
        /// </summary>
        public string AreaCode { get; set; }

            /// <summary>
        /// 城镇编码
        /// </summary>
        public string TownCode { get; set; }

            /// <summary>
        /// 省份编码
        /// </summary>
        public string ProvinceCode { get; set; }

    
    }
}
