using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.Common.Domain.Attachment;
using Tm.Module.Common.Domain.Attachment.Models;

namespace Tm.Module.Common.Infrastructure.Repositories.SqlServer
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
            query.WhereIf(model.Name.NotNull(), m => m.FileName.Contains(model.Name));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
