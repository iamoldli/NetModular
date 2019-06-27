using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.PersonnelFiles.Domain.UserContact
{
    /// <summary>
    /// 用户联系信息仓储
    /// </summary>
    public interface IUserContactRepository : IRepository<UserContactEntity>
    {
        /// <summary>
        /// 查询指定用户的联系信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserContactEntity> GetByUser(Guid userId);
    }
}
