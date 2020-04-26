using System.ComponentModel.DataAnnotations;

namespace NetModular.Module.Admin.Application.AuthService.ViewModels
{
    /// <summary>
    /// 邮箱登录
    /// </summary>
    public class EmailLoginModel : LoginModel
    {
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
