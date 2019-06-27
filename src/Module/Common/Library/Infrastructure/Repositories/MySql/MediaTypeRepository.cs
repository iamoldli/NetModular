using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Infrastructure.Repositories.MySql
{
    public class MediaTypeRepository : SqlServer.MediaTypeRepository
    {
        public MediaTypeRepository(IDbContext context) : base(context)
        {
        }
    }
}
