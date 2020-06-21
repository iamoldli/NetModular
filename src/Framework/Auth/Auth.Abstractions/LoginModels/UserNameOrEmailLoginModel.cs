using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 用户名或邮箱登录
    /// </summary>
    public class UserNameOrEmailLoginModel : LoginModel
    {
        /// <summary>
        /// 登录方式
        /// </summary>
        public override LoginMode Mode => LoginMode.UserNameOrEmail;

        /// <summary>
        /// 用户名或邮箱
        /// </summary>
        [Required(ErrorMessage = "请输入用户名或邮箱")]
        public string UserNameOrEmail { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
