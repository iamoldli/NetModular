using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.PersonnelFiles.Domain.Department;
using Nm.Module.PersonnelFiles.Domain.Department.Models;
using Nm.Module.PersonnelFiles.Domain.User;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
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
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));

            var joinQuery = query.LeftJoin<UserEntity>((x, y) => x.Leader == y.Id)
                .LeftJoin<AccountEntity>((x, y, z) => x.CreatedBy == z.Id);

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderBy((x, y, z) => x.Sort);
            }

            joinQuery.Select((x, y, z) => new { x, LeaderName = y.Name, Creator = z.Name });

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
            query.WhereNotEmpty(entity.ParentId, m => m.CompanyId == entity.CompanyId);
            query.WhereNotEmpty(entity.Id, m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<bool> ExistsChildren(Guid parentId)
        {
            return Db.Find(m => m.ParentId == parentId).ExistsAsync();
        }
    }
}
