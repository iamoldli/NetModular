using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Common.Domain.Attachment;
using Nm.Module.Common.Domain.Attachment.Models;

namespace Nm.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class AttachmentRepository : RepositoryAbstract<AttachmentEntity>, IAttachmentRepository
    {
        public AttachmentRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<AttachmentEntity>> Query(AttachmentQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereNotNull(model.Name, m => m.FileName.Contains(model.Name));

            var joinQuery = query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id);

            if (!paging.OrderBy.Any())
            {
                joinQuery.OrderByDescending((x, y) => x.Id);
            }

            joinQuery.Select((x, y) => new { x, Creator = y.Name });

            var result = await joinQuery.PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
