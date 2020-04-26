using System;
using NetModular.Module.Admin.Application.AuthService.ViewModels;

namespace NetModular.Module.Admin.Application.AuthService.Interfaces
{
    /// <summary>
    /// 验证码提供器
    /// </summary>
    public interface IVerifyCodeProvider
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="len">长度</param>
        /// <returns></returns>
        VerifyCodeModel Create(int len = 6);

        /// <summary>
        /// 检测
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel Check(LoginModel model);
    }

    public class VerifyCodeModel
    {
        /// <summary>
        /// 图片编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 验证码图片的Base64字符串
        /// </summary>
        public string Base64String { get; set; }
    }
}
