using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.AuthService.ViewModels
{
    /// <summary>
    /// 用户名或邮箱登录
    /// </summary>
    public class UserNameOrEmailLoginModel : LoginModel
    {
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
