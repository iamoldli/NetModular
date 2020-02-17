using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleOptionsService.ViewModels;

namespace NetModular.Module.Admin.Application.ModuleOptionsService
{
    /// <summary>
    /// 模块配置项服务接口
    /// </summary>
    public interface IModuleOptionsService
    {
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="moduleCode">模块编码</param>
        /// <returns></returns>
        IResultModel Edit(string moduleCode);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel Update(ModuleOptionsUpdateModel model);
    }
}
