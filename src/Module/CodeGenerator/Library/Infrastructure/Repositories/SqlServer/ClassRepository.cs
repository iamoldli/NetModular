using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.Class.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class ClassRepository : RepositoryAbstract<ClassEntity>, IClassRepository
    {
        public ClassRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ClassEntity>> Query(ClassQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find(m => m.ProjectId == model.ProjectId);
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name) || m.Remarks.Contains(model.Name));
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<ClassEntity>> QueryAllByProject(Guid projectId)
        {
            return Db.Find(m => m.ProjectId == projectId).ToListAsync();
        }

        public Task<bool> Exists(ClassEntity entity)
        {
            return Db.Find(m => m.ProjectId == entity.ProjectId && m.Name.Equals(entity.Name))
                .WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id)
                .ExistsAsync();
        }

        public Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ProjectId == projectId).UseTran(transaction).DeleteAsync();
        }
    }
}
