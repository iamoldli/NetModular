using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class FileOwnerRepository : SqlServer.FileOwnerRepository
    {
        public FileOwnerRepository(IDbContext context) : base(context)
        {
        }
    }
}
