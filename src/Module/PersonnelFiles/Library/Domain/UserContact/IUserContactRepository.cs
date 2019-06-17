using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Module.PersonnelFiles.Domain.UserContact.Models;

namespace Nm.Module.PersonnelFiles.Domain.UserContact
{
    /// <summary>
    /// 用户联系信息仓储
    /// </summary>
    public interface IUserContactRepository : IRepository<UserContactEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<UserContactEntity>> Query(UserContactQueryModel model);
    }
}
