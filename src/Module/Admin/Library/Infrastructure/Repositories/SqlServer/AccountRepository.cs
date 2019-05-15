using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class AccountRepository : RepositoryAbstract<AccountEntity>, IAccountRepository
    {
        public AccountRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> UpdatePassword(Guid id, string password)
        {
            return Db.Find(m => m.Id == id).UpdateAsync(m => new AccountEntity { Password = password });
        }

        public Task<AccountEntity> GetByUserName(string userName)
        {
            return GetAsync(m => m.Deleted == false && m.UserName.Equals(userName));
        }

        public Task<bool> UpdateLoginInfo(Guid id, string ip, AccountStatus status = AccountStatus.UnKnown)
        {
            var query = Db.Find(m => m.Id == id);
            if (status != AccountStatus.UnKnown)
            {
                return query.UpdateAsync(m => new AccountEntity { LoginIP = ip, LoginTime = DateTime.Now, Status = status }, false);
            }

            return query.UpdateAsync(m => new AccountEntity { LoginIP = ip, LoginTime = DateTime.Now }, false);
        }

        public Task<IList<AccountEntity>> Query(Paging paging, string userName = null, string name = null, string phone = null, string email = null)
        {
            var query = Db.Find(m => m.Deleted == false);
            query.WhereIf(userName.NotNull(), m => m.UserName.Contains(userName));
            query.WhereIf(name.NotNull(), m => m.Name.Contains(name));
            query.WhereIf(phone.NotNull(), m => m.Phone == phone);
            query.WhereIf(email.NotNull(), m => m.Email == email);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            return query.PaginationAsync(paging);
        }

        public Task<bool> ExistsUserName(string userName, Guid? id)
        {
            var query = Db.Find(m => m.Deleted == false && m.UserName == userName && m.UserName != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsPhone(string phone, Guid? id)
        {
            var query = Db.Find(m => m.Deleted == false && m.Phone == phone && m.Phone != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsEmail(string email, Guid? id)
        {
            var query = Db.Find(m => m.Deleted == false && m.Email == email && m.Email != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

    }
}
