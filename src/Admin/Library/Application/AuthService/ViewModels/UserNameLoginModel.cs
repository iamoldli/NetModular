using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.AuthService.ViewModels
{
    /// <summary>
    /// 用户名登录
    /// </summary>
    public class UserNameLoginModel : LoginModel
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
    }
}
