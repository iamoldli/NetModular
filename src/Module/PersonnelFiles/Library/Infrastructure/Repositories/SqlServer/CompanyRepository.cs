using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.PersonnelFiles.Domain.Company;
using Nm.Module.PersonnelFiles.Domain.Company.Models;

namespace Nm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
{
    public class CompanyRepository : RepositoryAbstract<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<CompanyEntity>> Query(CompanyQueryModel model)
        {
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereIf(model.Name.NotNull(), m => m.Name.Contains(model.Name));

            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var result = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
