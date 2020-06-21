namespace NetModular.Lib.Auth.Abstractions.LoginModels
{
    /// <summary>
    /// 自定义登录
    /// </summary>
    public class CustomLoginModel : LoginModel
    {
        public override LoginMode Mode => LoginMode.Custom;

        /// <summary>
        /// 登数Json数据
        /// </summary>
        public string JsonData { get; set; }
    }
}
