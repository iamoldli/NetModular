using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
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
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name) || m.Remarks.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var list = await joinQuery.PaginationAsync(paging);
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
                .WhereNotEmpty(entity.Id, m => m.Id != entity.Id)
                .ExistsAsync();
        }

        public Task<bool> DeleteByProject(Guid projectId, IUnitOfWork uow)
        {
            return Db.Find(m => m.ProjectId == projectId).UseUow(uow).DeleteAsync();
        }
    }
}
