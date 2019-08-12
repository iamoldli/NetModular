using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Module.Common.Domain.AttachmentOwner;

namespace Tm.Module.Common.Infrastructure.Repositories.SqlServer
{
    public class AttachmentOwnerRepository : RepositoryAbstract<AttachmentOwnerEntity>, IAttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exist(AttachmentOwnerEntity entity)
        {
            return Db.Find(m => m.AccountId == entity.AccountId && m.AttachmentId == entity.AttachmentId).ExistsAsync();
        }
    }
}
