using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Module.PersonnelFiles.Domain.UserContact;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
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
