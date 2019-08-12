using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Admin.Domain.Permission;
using Tm.Module.Admin.Domain.Permission.Models;

namespace Tm.Module.Admin.Application.PermissionService
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(PermissionQueryModel model);

        /// <summary>
        /// 同步
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Sync(List<PermissionEntity> permissions);
    }
}
