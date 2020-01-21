using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountAuthInfo;

namespace NetModular.Module.Admin.Application.AuthService.ResultModels
{
    public class LoginResultModel
    {
        /// <summary>
        /// 账户信息
        /// </summary>
        public AccountEntity Account { get; set; }

        /// <summary>
        /// 认证信息
        /// </summary>
        public AccountAuthInfoEntity AuthInfo { get; set; }
    }
}
