using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Admin.Domain.UserInfo
{
    /// <summary>
    /// 用户信息仓储
    /// </summary>
    public interface IUserInfoRepository : IRepository<UserInfoEntity>
    {
        /// <summary>
        /// 判断是否存在绑定了指定部门的用户
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        Task<bool> ExistsByDept(Guid deptId);
    }
}
