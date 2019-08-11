using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AttachmentRepository : SqlServer.AttachmentRepository
    {
        public AttachmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
