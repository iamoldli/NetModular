using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 手机号登录
    /// </summary>
    public class PhoneLoginModel : LoginModel
    {
        /// <summary>
        /// 登录方式
        /// </summary>
        public override LoginMode Mode => LoginMode.Phone;

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; } = "086";

        /// <summary>
        /// Phone
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        public string Phone { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "请输入验证码")]
        public string Code { get; set; }
    }
}
