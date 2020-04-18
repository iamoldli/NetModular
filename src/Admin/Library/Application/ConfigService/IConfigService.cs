using NetModular.Lib.Config.Abstractions;
using NetModular.Module.Admin.Application.ConfigService.ResultModels;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;

namespace NetModular.Module.Admin.Application.ConfigService
{
    /// <summary>
    /// 配置服务
    /// </summary>
    public interface IConfigService
    {
        /// <summary>
        /// 获取UI配置信息
        /// </summary>
        /// <returns></returns>
        UIConfigResultModel GetUI();

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="code">配置类编码</param>
        /// <param name="type">配置类类型</param>
        /// <returns></returns>
        IResultModel Edit(string code, ConfigType type);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel Update(ConfigUpdateModel model);
    }
}
