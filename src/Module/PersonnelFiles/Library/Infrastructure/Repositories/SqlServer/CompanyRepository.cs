using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Core;
using Tm.Lib.Data.Query;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Module.Admin.Domain.Account;
using Tm.Module.PersonnelFiles.Domain.Company;
using Tm.Module.PersonnelFiles.Domain.Company.Models;

namespace Tm.Module.PersonnelFiles.Infrastructure.Repositories.SqlServer
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

            var result = await query.InnerJoin<AccountEntity>((x, y) => (x.Id == y.CID || y.CID.Equals("00000000-0000-0000-0000-000000000000")) && (y.Id==model.AccountID))
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;

            return result;
        }
    }
}
