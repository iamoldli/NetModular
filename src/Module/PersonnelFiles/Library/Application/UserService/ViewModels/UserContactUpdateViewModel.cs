using System;
using System.ComponentModel.DataAnnotations;
using Tm.Lib.Utils.Core.Models;

namespace Tm.Module.PersonnelFiles.Application.UserService.ViewModels
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
        /// 区域信息
        /// </summary>
        public AreaSelectModel Area { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
    }
}
