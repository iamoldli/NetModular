using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 邮箱登录
    /// </summary>
    public class EmailLoginModel : LoginModel
    {
        public override LoginMode Mode => LoginMode.Email;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage = "请输入邮箱")]
        [EmailAddress(ErrorMessage = "请输入正确的邮箱格式")]
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
