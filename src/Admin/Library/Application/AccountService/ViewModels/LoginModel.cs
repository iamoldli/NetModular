using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Module.Admin.Application.AccountService.ViewModels
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 账户类型
        /// </summary>
        [Required(ErrorMessage = "请选择账户类型")]
        public AccountType AccountType { get; set; } = AccountType.Admin;

        /// <summary>
        /// 平台
        /// </summary>
        public Platform Platform { get; set; }

        /// <summary>
        /// 验证码图片编号
        /// </summary>
        public string PictureId { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [JsonIgnore]
        public string IP { get; set; }
    }
}
