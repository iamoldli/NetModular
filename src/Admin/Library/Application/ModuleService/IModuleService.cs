using System.Threading.Tasks;
using NetModular.Module.Admin.Application.ModuleService.ViewModels;

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
        Task<IResultModel> Sync();

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        IResultModel Select();

        /// <summary>
        /// 模块配置项编辑
        /// </summary>
        /// <param name="code">模块编码</param>
        /// <returns></returns>
        IResultModel OptionsEdit(string code);

        /// <summary>
        /// 模块配置项更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        IResultModel OptionsUpdate(ModuleOptionsUpdateModel model);

        /// <summary>
        /// 同步接口请求数量
        /// </summary>
        /// <returns></returns>
        Task<bool> SyncApiRequestCount();
    }
}
