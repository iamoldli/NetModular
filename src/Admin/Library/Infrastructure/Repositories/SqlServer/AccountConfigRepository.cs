using System;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.AccountConfig;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AccountConfigRepository : RepositoryAbstract<AccountConfigEntity>, IAccountConfigRepository
    {
        public AccountConfigRepository(IDbContext context) : base(context)
        {
        }

        public Task<AccountConfigEntity> GetByAccount(Guid accountId)
        {
            return GetAsync(m => m.AccountId == accountId);
        }
    }
}
