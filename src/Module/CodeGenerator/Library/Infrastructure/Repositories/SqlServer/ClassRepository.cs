using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.CodeGenerator.Domain.Class;
using NetModular.Module.CodeGenerator.Domain.Class.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
    }
}
