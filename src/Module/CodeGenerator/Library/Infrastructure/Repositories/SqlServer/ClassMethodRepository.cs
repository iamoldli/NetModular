using System;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Module.CodeGenerator.Domain.ClassMethod;

namespace Tm.Module.CodeGenerator.Infrastructure.Repositories.SqlServer
{
    public class ClassMethodRepository : RepositoryAbstract<ClassMethodEntity>, IClassMethodRepository
    {
        public ClassMethodRepository(IDbContext context) : base(context)
        {

        }

        public Task<bool> DeleteByClass(Guid classId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ClassId == classId).UseTran(transaction).DeleteAsync();
        }

        public Task<ClassMethodEntity> GetByClass(Guid classId, IDbTransaction transaction)
        {
            return Db.Find(m => m.ClassId == classId).UseTran(transaction).FirstAsync();
        }
    }
}
