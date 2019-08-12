using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.PersonnelFiles.Domain.Company.Models;

namespace Tm.Module.PersonnelFiles.Domain.Company
{
    /// <summary>
    /// 公司单位仓储
    /// </summary>
    public interface ICompanyRepository : IRepository<CompanyEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<CompanyEntity>> Query(CompanyQueryModel model);
    }
}
