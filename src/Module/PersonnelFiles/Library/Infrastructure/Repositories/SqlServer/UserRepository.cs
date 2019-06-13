using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.PersonnelFiles.Domain.User;
using Nm.Module.PersonnelFiles.Domain.User.Models;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class UserRepository : RepositoryAbstract<UserEntity>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<UserEntity>> Query(UserQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> ExistsBindDept(Guid departmentId)
        {
            return Db.Find(m => m.Deleted == false && m.DepartmentId == departmentId).ExistsAsync();
        }
    }
}
