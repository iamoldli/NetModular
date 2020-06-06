using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RoleButton
{
    /// <summary>
    /// 角色按钮绑定仓储
    /// </summary>
    public interface IRoleButtonRepository : IRepository<RoleButtonEntity>
    {
        /// <summary>
        /// 查询指定角色的所有按钮列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<RoleButtonEntity>> QueryButtonCodes(Guid roleId);

        /// <summary>
        /// 查询指定角色和页面的所有按钮编码列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="pageCode">页面编码</param>
        /// <returns></returns>
        Task<IList<string>> QueryButtonCodes(Guid roleId, string pageCode);

        /// <summary>
        /// 查询指定账户的按钮列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<string>> QueryButtonCodesByAccount(Guid accountId);

        /// <summary>
        /// 删除指定角色绑定的按钮
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="uow">工作单元</param>
        /// <returns></returns>
        Task<bool> DeleteByRole(Guid roleId, IUnitOfWork uow = null);
    }
}
