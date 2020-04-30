using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Core;
using NetModular.Module.Admin.Domain.FileOwner;

namespace NetModular.Module.Admin.Infrastructure.Repositories.SqlServer
{
    public class FileOwnerRepository : RepositoryAbstract<FileOwnerEntity>, IFileOwnerRepository
    {
        public FileOwnerRepository(IDbContext context) : base(context)
        {
        }

        public Task<bool> Exist(FileOwnerEntity entity)
        {
            return Db.Find(m => m.SaveId == entity.SaveId && m.AccountId == entity.AccountId).ExistsAsync();
        }
    }
}
