using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.Common.Domain.AttachmentOwner;

namespace Nm.Module.Common.Infrastructure.Repositories.SqlServer
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
