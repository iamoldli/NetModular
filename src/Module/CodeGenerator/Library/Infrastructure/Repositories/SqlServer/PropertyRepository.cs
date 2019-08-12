using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.CodeGenerator.Domain.Enum;
using Tm.Module.CodeGenerator.Domain.Property;
using Tm.Module.CodeGenerator.Domain.Property.Models;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name) || m.Remarks.Contains(model.Name));
            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Sort);
                query.OrderByDescending(m => m.IsInherit);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .LeftJoin<EnumEntity>((x, y, z) => x.EnumId == z.Id)
                .Select((x, y, z) => new { x, Creator = y.Name, EnumName = z.Name })
                .PaginationAsync(paging);
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
                .WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id)
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
