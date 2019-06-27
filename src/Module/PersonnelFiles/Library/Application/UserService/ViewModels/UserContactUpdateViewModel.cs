using System;
using System.ComponentModel.DataAnnotations;

namespace Nm.Module.PersonnelFiles.Application.UserService.ViewModels
{
    public class UserContactUpdateViewModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Required(ErrorMessage = "请选择用户")]
        public Guid UserId { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
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
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区县编码
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// 区县名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 城镇编码
        /// </summary>
        public string TownCode { get; set; }

        /// <summary>
        /// 城镇名称
        /// </summary>
        public string TownName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
    }
}
