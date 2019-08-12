using System;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Module.PersonnelFiles.Domain.UserContact;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class UserContactRepository : RepositoryAbstract<UserContactEntity>, IUserContactRepository
    {
        public UserContactRepository(IDbContext context) : base(context)
        {
        }

        public Task<UserContactEntity> GetByUser(Guid userId)
        {
            return Db.Find(m => m.UserId == userId).FirstAsync();
        }
    }
}
