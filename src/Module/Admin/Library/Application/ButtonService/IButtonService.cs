using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ButtonService.ViewModels;

namespace NetModular.Module.Admin.Application.ButtonService
{
    /// <summary>
    /// 按钮服务
    /// </summary>
    public interface IButtonService
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ButtonQueryModel model);

        /// <summary>
        /// 同步
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Sync(ButtonSyncModel model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 查询绑定的权限列表
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> QueryPermissionList(Guid id);

        /// <summary>
        /// 绑定权限
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> BindPermission(ButtonBindPermissionModel model);
    }
}
