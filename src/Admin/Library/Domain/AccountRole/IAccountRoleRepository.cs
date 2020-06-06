using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.AccountRole
{
    /// <summary>
    /// 账户角色关联仓储
    /// </summary>
    public interface IAccountRoleRepository : IRepository<AccountRoleEntity>
    {
        /// <summary>
        /// 删除账户绑定的指定角色信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> Delete(Guid accountId, Guid roleId);

        /// <summary>
        /// 删除指定账户的关联信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> DeleteByAccount(Guid accountId, IUnitOfWork uow);

        /// <summary>
        /// 判断绑定关系是否已存在
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> Exists(Guid accountId, Guid roleId);

        /// <summary>
        /// 查询指定账户关联的角色列表
        /// </summary>
        /// <returns></returns>
        Task<IList<Role.RoleEntity>> QueryRole(Guid accountId);

        /// <summary>
        /// 查询指定角色的关联列表
        /// </summary>
        /// <returns></returns>
        Task<IList<AccountRoleEntity>> QueryByRole(Guid roleId);

        /// <summary>
        /// 查询指定菜单的关联列表
        /// </summary>
        /// <returns></returns>
        Task<IList<AccountRoleEntity>> QueryByMenu(Guid menuId);

        /// <summary>
        /// 判断指定的角色是否有绑定关系
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<bool> ExistsByRole(Guid roleId);
    }
}
