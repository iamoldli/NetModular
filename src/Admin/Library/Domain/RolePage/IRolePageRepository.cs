using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RolePage
{
    /// <summary>
    /// 角色页面绑定关系仓储
    /// </summary>
    public interface IRolePageRepository : IRepository<RolePageEntity>
    {
        /// <summary>
        /// 查询指定角色的页面编码列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<string>> QueryPageCodesByRole(Guid roleId);

        /// <summary>
        /// 查询指定账户的页面编码列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<string>> QueryPageCodesByAccount(Guid accountId);

        /// <summary>
        /// 删除指定角色绑定的页面
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> DeleteByRole(Guid roleId, IUnitOfWork uow = null);
    }
}
