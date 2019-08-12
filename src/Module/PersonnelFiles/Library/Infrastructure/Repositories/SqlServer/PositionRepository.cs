using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.PersonnelFiles.Domain.Company;
using Tm.Module.PersonnelFiles.Domain.Department;
using Tm.Module.PersonnelFiles.Domain.Position;
using Tm.Module.PersonnelFiles.Domain.Position.Models;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class PositionRepository : RepositoryAbstract<PositionEntity>, IPositionRepository
    {
        public PositionRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<PositionEntity>> Query(PositionQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereIf(model.DepartmentId.NotEmpty(), m => m.DepartmentId == model.DepartmentId);
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(m.Name));
            query.WhereIf(model.Code.NotNull(), m => m.Code == model.Code);

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .LeftJoin<DepartmentEntity>((x, y, z) => x.DepartmentId == z.Id)
                .LeftJoin<CompanyEntity>((x, y, z, m) => z.CompanyId == m.Id)
                .Select((x, y, z, m) => new { x, DepartmentName = z.Name, CompanyName = m.Name, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }

        public Task<bool> Exists(PositionEntity entity)
        {
            var query = Db.Find(m => m.DepartmentId == entity.DepartmentId);

            if (entity.Code.NotNull())
            {
                query.Where(m => m.Name == entity.Name || m.Code == entity.Code);
            }
            else
            {
                query.Where(m => m.Name == entity.Name);
            }

            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);

            return query.ExistsAsync();
        }

        public Task<IList<PositionEntity>> QueryByDepartment(Guid departmentId)
        {
            return Db.Find(m => m.DepartmentId == departmentId).ToListAsync();
        }
    }
}
