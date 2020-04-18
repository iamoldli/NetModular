using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Module.Admin.Application.ModuleService.ViewModels;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.ModuleService
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Query();

        /// <summary>
        /// 同步模块信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Sync(List<PermissionEntity> permissions);

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        IResultModel Select();

        /// <summary>
        /// 同步接口请求数量
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncApiRequestCount();
    }
}
