using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.Account.Models;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
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

        public Task<AccountEntity> GetByUserName(string userName, AccountType type)
        {
            return GetAsync(m => m.UserName.Equals(userName) && m.Type == type);
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

        public async Task<IList<AccountEntity>> Query(AccountQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereIf(model.Type != null, m => m.Type == model.Type.Value);
            query.WhereNotNull(model.UserName, m => m.UserName.Contains(model.UserName));
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            query.WhereNotNull(model.Phone, m => m.Phone == model.Phone);
            query.WhereNotNull(model.Email, m => m.Email == model.Email);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> ExistsUserName(string userName, Guid? id, AccountType type = AccountType.Admin)
        {
            var query = Db.Find(m => m.Type == type && m.UserName == userName);
            query.WhereNotNull(id, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsPhone(string phone, Guid? id, AccountType type = AccountType.Admin)
        {
            var query = Db.Find(m => m.Type == type && m.Phone == phone);
            query.WhereNotNull(id, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsEmail(string email, Guid? id, AccountType type = AccountType.Admin)
        {
            var query = Db.Find(m => m.Type == type && m.Email == email);
            query.WhereNotNull(id, m => m.Id != id);
            return query.ExistsAsync();
        }
    }
}
