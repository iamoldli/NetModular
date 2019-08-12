using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Infrastructure.Repositories.MySql
{
    public class AttachmentOwnerRepository : SqlServer.AttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
