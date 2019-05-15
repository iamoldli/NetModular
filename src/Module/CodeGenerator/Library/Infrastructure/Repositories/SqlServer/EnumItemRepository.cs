using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.CodeGenerator.Domain.EnumItem;
using NetModular.Module.CodeGenerator.Domain.EnumItem.Models;

namespace NetModular.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
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
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));
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
                .WhereIf(entity.Id.NotEmpty(), m => m.Id != entity.Id)
                .ExistsAsync();
        }
    }
}
