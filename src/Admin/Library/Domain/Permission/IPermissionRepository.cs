using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Permission.Models;

namespace NetModular.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        /// <summary>
        /// 根据模块编码判断是否存在
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthModule(string moduleCode);

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<IList<PermissionEntity>> Query(PermissionQueryModel model);

        /// <summary>
        /// 查询指定账户的权限编码列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="platform">平台</param>
        /// <returns></returns>
        Task<IList<string>> QueryCodeByAccount(Guid accountId, Platform platform);
    }
}
