using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Department;
using Nm.Module.Admin.Domain.Department.Models;

namespace Nm.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class DepartmentRepository : RepositoryAbstract<DepartmentEntity>, IDepartmentRepository
    {
        public DepartmentRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<DepartmentEntity>> Query(DepartmentQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find(m => m.ParentId == model.ParentId);
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Sort);
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(DepartmentEntity entity)
        {
            var query = Db.Find(m => m.ParentId == entity.ParentId && m.Name.Equals(entity.Name));
            query.WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id);
            return query.ExistsAsync();
        }

        public Task<bool> ExistsChild(Guid id)
        {
            return Db.Find(m => m.ParentId == id).ExistsAsync();
        }
    }
}
