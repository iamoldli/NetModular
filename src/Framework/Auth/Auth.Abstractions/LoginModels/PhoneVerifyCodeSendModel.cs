using System.ComponentModel.DataAnnotations;

namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 手机验证码发送模型
    /// </summary>
    public class PhoneVerifyCodeSendModel
    {
        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode { get; set; } = "086";

        /// <summary>
        /// Phone
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        public string Phone { get; set; }
    }
}
