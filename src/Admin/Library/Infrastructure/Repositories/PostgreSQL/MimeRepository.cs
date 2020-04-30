using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.PostgreSQL
{
    public class MimeRepository : SqlServer.MimeRepository
    {
        public MimeRepository(IDbContext context) : base(context)
        {
        }
    }
}
