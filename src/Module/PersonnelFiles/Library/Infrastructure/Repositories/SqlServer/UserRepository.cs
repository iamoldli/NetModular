using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.PersonnelFiles.Domain.Department;
using Nm.Module.PersonnelFiles.Domain.Position;
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
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
            query.WhereIf(model.Number != null && model.Number > 0, m => m.JobNo == model.Number);
            query.WhereIf(!model.CID.IsEmpty() , m => m.CID == model.CID);
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .LeftJoin<DepartmentEntity>((t1, t2, t3) => t1.DepartmentId == t3.Id)
                .LeftJoin<PositionEntity>((t1, t2, t3, t4) => t1.PositionId == t4.Id)
                .Select((t1, t2, t3, t4) => new { t1, Creator = t2.Name, DepartmentName = t3.Name, PositionName = t4.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> ExistsBindDept(Guid departmentId)
        {
            return Db.Find(m => m.Deleted == false && m.DepartmentId == departmentId).ExistsAsync();
        }

        public Task<bool> ExistsBindPosition(Guid positionId)
        {
            return Db.Find(m => m.Deleted == false && m.DepartmentId == positionId).ExistsAsync();
        }

        public Task<int> GetMaxJobNumber()
        {
            return Db.Find().MaxAsync(m => m.JobNo);
        }
    }
}
