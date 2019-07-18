using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.MySql
{
    public class AttachmentOwnerRepository : SqlServer.AttachmentOwnerRepository
    {
        public AttachmentOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
