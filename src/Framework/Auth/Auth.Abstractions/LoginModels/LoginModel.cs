using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Auth.Abstractions.Providers;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 登录模型
    /// </summary>
    public abstract class LoginModel
    {
        /// <summary>
        /// 登录模式
        /// </summary>
        [IgnoreProperty]
        public abstract LoginMode Mode { get; }

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

        /// <summary>
        /// 客户IP地址
        /// </summary>
        [IgnoreProperty]
        public string IP { get; set; }

        /// <summary>
        /// 客户浏览器UA(Web端)
        /// </summary>
        [IgnoreProperty]
        public string UserAgent { get; set; }
    }
}
