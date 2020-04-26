using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.LoginLog.Models;

namespace NetModular.Module.Admin.Application.LogService
{
    /// <summary>
    /// 日志服务
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 查询登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> QueryLogin(LoginLogQueryModel model);

        /// <summary>
        /// 导出登录日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel<ExcelExportResultModel>> ExportLogin(LoginLogQueryModel model);
    }
}
