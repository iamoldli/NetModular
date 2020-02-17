using System;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.AccountAuthInfo;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AccountAuthInfoRepository : RepositoryAbstract<AccountAuthInfoEntity>, IAccountAuthInfoRepository
    {
        public AccountAuthInfoRepository(IDbContext context) : base(context)
        {
        }

        public Task<AccountAuthInfoEntity> Get(Guid accountId, Platform platform)
        {
            return Db.Find(m => m.AccountId == accountId && m.Platform == platform).FirstAsync();
        }

        public Task<AccountAuthInfoEntity> GetByRefreshToken(string refreshToken)
        {
            return Db.Find(m => m.RefreshToken == refreshToken).FirstAsync();
        }
    }
}
