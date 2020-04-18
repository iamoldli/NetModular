using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Module.Admin.Application.AuthService.ResultModels;
using NetModular.Module.Admin.Application.AuthService.ViewModels;
using NetModular.Module.Admin.Domain.AccountAuthInfo;

namespace NetModular.Module.Admin.Application.AuthService
{
    /// <summary>
    /// 身份认证服务接口
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        IResultModel CreateVerifyCode(int length = 6);

        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="model">登录模型</param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> Login(LoginModel model);

        /// <summary>
        /// 刷新令牌(只针对JWT认证方式)
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<ResultModel<LoginResultModel>> RefreshToken(string refreshToken);

        /// <summary>
        /// 获取认证信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetAuthInfo();

        /// <summary>
        /// 查询指定账户的认证信息(缓存优先)
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<AccountAuthInfoEntity> GetAuthInfo(Guid accountId, Platform platform);
    }
}
