using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.AccountAuthInfo
{
    public interface IAccountAuthInfoRepository : IRepository<AccountAuthInfoEntity>
    {
        /// <summary>
        /// 查询指定账户和平台的登录信息
        /// </summary>
        /// <param name="accountId">账户编号</param>
        /// <param name="platform">平台</param>
        /// <returns></returns>
        Task<AccountAuthInfoEntity> Get(Guid accountId, Platform platform);

        /// <summary>
        /// 根据刷新令牌查询
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<AccountAuthInfoEntity> GetByRefreshToken(string refreshToken);
    }
}
