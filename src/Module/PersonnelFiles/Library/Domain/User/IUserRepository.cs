using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.PersonnelFiles.Domain.User.Models;

namespace Nm.Module.PersonnelFiles.Domain.User
{
    /// <summary>
    /// 用户信息仓储
    /// </summary>
    public interface IUserRepository : IRepository<UserEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<UserEntity>> Query(UserQueryModel model);

        /// <summary>
        /// 判断是否有人员绑定了指定部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<bool> ExistsBindDept(Guid departmentId);
    }
}
