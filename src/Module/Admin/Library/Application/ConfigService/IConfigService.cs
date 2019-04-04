using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;

namespace NetModular.Module.Admin.Application.ConfigService
{
    /// <summary>
    /// 配置项服务
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> GetSystemConfig();

        /// <summary>
        /// 更改系统配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel UpdateSystemConfig(SystemConfigModel model);
    }
}
