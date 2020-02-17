namespace NetModular.Module.Admin.Application.AuthService.ResultModels
{
    public class VerifyCodeResultModel
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 验证码图片的Base64字符串
        /// </summary>
        public string Base64String { get; set; }
    }
}
