using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Swagger.Abstractions.Attributes;

namespace NetModular.Module.Admin.Application.AuthService.ViewModels
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public class LoginModel
    {
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
        /// 验证码
        /// </summary>
        public VerifyCodeModel VerifyCode { get; set; }

        [IgnoreProperty]
        public string IP { get; set; }

        [IgnoreProperty]
        public string UserAgent { get; set; }
    }
}
