using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.RolePlatformPermission
{
    public interface IRolePlatformPermissionRepository : IRepository<RolePlatformPermissionEntity>
    {
        /// <summary>
        /// 查询指定角色和平台的权限绑定列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<IList<RolePlatformPermissionEntity>> Query(Guid roleId, Platform platform);

        /// <summary>
        /// 清除指定角色和平台的所有权限绑定列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="platform"></param>
        /// <returns></returns>
        Task<bool> Clear(Guid roleId, Platform platform);
    }
}
