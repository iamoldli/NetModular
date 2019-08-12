using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AttachmentOwnerRepository : SqlServer.AttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
