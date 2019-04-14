using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.PermissionService.ViewModels;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.PermissionService
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
        Task<IResultModel> Sync(List<Permission> permissions);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);
    }
}
