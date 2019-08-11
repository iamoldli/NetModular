using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
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

        public Task<AccountEntity> GetByUserName(string userName, int type = 0)
        {
            return GetAsync(m => m.Deleted == false && m.UserName.Equals(userName) );
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
            var query = Db.Find(m => m.Deleted == false );
            query.WhereIf(model.UserName.NotNull(), m => m.UserName.Contains(model.UserName));
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Phone.NotNull(), m => m.Phone == model.Phone);
            query.WhereIf(model.Email.NotNull(), m => m.Email == model.Email);
            query.WhereIf(!model.CID.IsEmpty(), m => m.CID == model.CID);
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> ExistsUserName(string userName, Guid? id, int type = 0)
        {
            var query = Db.Find(m => m.Deleted == false && m.Type == type && m.UserName == userName && m.UserName != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsPhone(string phone, Guid? id, int type = 0)
        {
            var query = Db.Find(m => m.Deleted == false && m.Type == type && m.Phone == phone && m.Phone != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsEmail(string email, Guid? id, int type = 0)
        {
            var query = Db.Find(m => m.Deleted == false && m.Type == type && m.Email == email && m.Email != null);
            query.WhereIf(id != null, m => m.Id != id);
            return query.ExistsAsync();
        }
    }
}
