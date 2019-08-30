using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.CodeGenerator.Domain.EnumItem;
using Nm.Module.CodeGenerator.Domain.EnumItem.Models;

namespace Nm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class EnumItemRepository : RepositoryAbstract<EnumItemEntity>, IEnumItemRepository
    {
        public EnumItemRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<EnumItemEntity>> Query(EnumItemQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find(m => m.EnumId == model.EnumId);
            query.WhereNotNull(model.Name, m => m.Name.Contains(model.Name));
            if (!paging.OrderBy.Any())
            {
                query.OrderBy(m => m.Value);
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<IList<EnumItemEntity>> QueryByEnum(Guid enumId)
        {
            return Db.Find(m => m.EnumId == enumId).OrderBy(m => m.Value).ToListAsync();
        }

        public Task<bool> Exists(EnumItemEntity entity)
        {
            return Db.Find(m => m.EnumId == entity.EnumId && (m.Name.Equals(entity.Name) || m.Value == entity.Value))
                .WhereNotEmpty(entity.Id, m => m.Id != entity.Id)
                .ExistsAsync();
        }
    }
}
