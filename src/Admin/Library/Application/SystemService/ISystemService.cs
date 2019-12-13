using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;

namespace NetModular.Module.Admin.Application.SystemService
{
    /// <summary>
    /// 系统相关服务
    /// </summary>
    public interface ISystemService
    {
        /// <summary>
        /// 更改基础配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdateBaseConfig(SystemBaseConfigModel model);

        /// <summary>
        /// 更改组件配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdateComponentConfig(SystemComponentConfigModel model);

        /// <summary>
        /// 更改登录配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdateLoginConfig(SystemLoginConfigModel model);

        /// <summary>
        /// 更改权限配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdatePermissionConfig(SystemPermissionConfigModel model);

        /// <summary>
        /// 更改工具栏配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdateToolbarConfig(SystemToolbarConfigModel model);
    }
}
