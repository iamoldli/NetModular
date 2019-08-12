using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Infrastructure.Repositories.SQLite
{
    public class AttachmentRepository : SqlServer.AttachmentRepository
    {
        public AttachmentRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
