using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.CodeGenerator.Domain.Enum;
using Nm.Module.CodeGenerator.Domain.Property;
using Nm.Module.CodeGenerator.Domain.Property.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class PropertyRepository : RepositoryAbstract<PropertyEntity>, IPropertyRepository
    {
        public PropertyRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<PropertyEntity>> Query(PropertyQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find(m => m.ClassId == model.ClassId);
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name) || m.Remarks.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .LeftJoin<EnumEntity>((x, y, z) => x.EnumId == z.Id);

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderBy((x, y, z) => x.Sort);
                joinQuery.OrderByDescending((x, y, z) => x.IsInherit);
            }

            joinQuery.Select((x, y, z) => new { x, Creator = y.Name, EnumName = z.Name });

            var list = await joinQuery.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<PropertyEntity>> QueryByClass(Guid classId)
        {
            return Db.Find(m => m.ClassId == classId).OrderBy(m => m.Sort).ToListAsync();
        }

        public Task<bool> Exists(PropertyEntity entity)
        {
            return Db.Find(m => m.ClassId == entity.ClassId && m.Name.Equals(entity.Name))
                .WhereNotEmpty(entity.Id, m => m.Id != entity.Id)
                .ExistsAsync();
        }

        public Task<bool> ExistsByEnum(Guid enumId)
        {
            return Db.Find(m => m.EnumId == enumId).ExistsAsync();
        }

        public Task<bool> DeleteByClass(Guid classId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ClassId == classId).UseTran(transaction).DeleteAsync();
        }

        public Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ProjectId == projectId).UseTran(transaction).DeleteAsync();
        }
    }
}
