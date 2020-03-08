using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.Permission.Models;

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
        Task<IResultModel> Sync(List<PermissionEntity> permissions);

        /// <summary>
        /// 获取权限树形结构
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetTree();

        /// <summary>
        /// 根据编码查询
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        Task<IResultModel> QueryByCodes(List<string> codes);
    }
}
