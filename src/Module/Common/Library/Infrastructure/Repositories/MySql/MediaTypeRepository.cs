using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Infrastructure.Repositories.MySql
{
    public class MediaTypeRepository : SqlServer.MediaTypeRepository
    {
        public MediaTypeRepository(IDbContext context) : base(context)
        {
        }
    }
}
