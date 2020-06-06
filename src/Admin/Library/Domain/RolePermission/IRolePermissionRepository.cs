using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RolePermission
{
    /// <summary>
    /// 角色权限关联仓储
    /// </summary>
    public interface IRolePermissionRepository : IRepository<RolePermissionEntity>
    {
        /// <summary>
        /// 查询指定角色和平台的权限列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IList<string>> QueryByRole(Guid roleId, Platform platform);

        /// <summary>
        /// 查询指定账户和平台的权限列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IList<string>> QueryByAccount(Guid accountId, Platform platform);

        /// <summary>
        /// 删除指定角色的权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="platform">平台</param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> DeleteByRole(Guid roleId, Platform platform, IUnitOfWork uow = null);
    }
}
