using System;
using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Domain.ModuleInfo.Models;

namespace Nm.Module.Admin.Application.ModuleInfoService
{
    /// <summary>
    /// 模块服务
    /// </summary>
    public interface IModuleInfoService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(ModuleInfoQueryModel model);

        /// <summary>
        /// 同步模块信息
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Sync();

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);

        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> Select();
    }
}
