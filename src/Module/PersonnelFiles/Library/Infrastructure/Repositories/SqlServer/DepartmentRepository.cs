using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.PersonnelFiles.Domain.Department;
using Tm.Module.PersonnelFiles.Domain.Department.Models;
using Tm.Module.PersonnelFiles.Domain.User;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class DepartmentRepository : RepositoryAbstract<DepartmentEntity>, IDepartmentRepository
    {
        public DepartmentRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<DepartmentEntity>> Query(DepartmentQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.CompanyId == model.CompanyId);

            var parentId = model.ParentId ?? Guid.Empty;
            query.Where(m => m.ParentId == parentId);
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Sort);
            }

            var joinQuery = query.LeftJoin<UserEntity>((x, y) => x.Leader == y.Id)
                .LeftJoin<AccountEntity>((x, y, z) => x.CreatedBy == z.Id)
                .Select((x, y, z) => new { x, LeaderName = y.Name, Creator = z.Name });

            var result = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<IList<DepartmentEntity>> QueryAllByCompany(Guid companyId)
        {
            return Db.Find(m => m.CompanyId == companyId).ToListAsync();
        }

        public Task<bool> Exists(DepartmentEntity entity)
        {
            var query = Db.Find(m => m.Name == entity.Name && m.ParentId == entity.ParentId);
            query.WhereIf(entity.ParentId.IsEmpty(), m => m.CompanyId == entity.CompanyId);
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            
            return query.ExistsAsync();
        }

        public Task<bool> ExistsChildren(Guid parentId)
        {
            return Db.Find(m => m.ParentId == parentId).ExistsAsync();
        }
    }
}
