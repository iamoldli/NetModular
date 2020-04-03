using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Account.Models;

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

        public Task<AccountEntity> GetByUserName(string userName, AccountType type, Guid tenantId)
        {
            return GetAsync(m => m.UserName.Equals(userName) && m.Type == type && m.TenantId == tenantId);
        }

        public Task<bool> UpdateAccountStatus(Guid id, AccountStatus status, IUnitOfWork uow = null)
        {
            return Db.Find(m => m.Id == id).UseUow(uow).UpdateAsync(m => new AccountEntity { Status = status });
        }

        public async Task<IList<AccountEntity>> Query(AccountQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Type, m => m.Type == model.Type.Value);
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
