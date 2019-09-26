using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Admin.Domain.AccountConfig;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
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
