using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Common.Domain.MediaType;
using Nm.Module.Common.Domain.MediaType.Models;

namespace Nm.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class MediaTypeRepository : RepositoryAbstract<MediaTypeEntity>, IMediaTypeRepository
    {
        public MediaTypeRepository(IDbContext context) : base(context)
        {
        }

        public Task<MediaTypeEntity> GetByExt(string ext)
        {
            return Db.Find(m => m.Ext == ext).FirstAsync();
        }

        public async Task<IList<MediaTypeEntity>> Query(MediaTypeQueryModel model)
        {
            var paging = model.Paging();
            var query = Db.Find();
            query.WhereNotNull(model.Ext, m => m.Ext.Contains(model.Ext));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.PaginationAsync(paging);
            model.TotalCount = paging.TotalCount;
            return list;
        }

        public Task<bool> Exists(string ext, int id = 0)
        {
            var query = Db.Find(m => m.Ext == ext);
            query.WhereIf(id > 0, m => m.Id != id);
            return query.ExistsAsync();
        }
    }
}
