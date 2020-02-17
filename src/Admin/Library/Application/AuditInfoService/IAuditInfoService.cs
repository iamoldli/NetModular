using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.AuditInfo.Models;

namespace NetModular.Module.Admin.Application.AuditInfoService
{
    /// <summary>
    /// 审计服务
    /// </summary>
    public interface IAuditInfoService
    {
        /// <summary>
        /// 添加审计信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        Task<IResultModel> Add(AuditInfoEntity info);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AuditInfoQueryModel model);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Details(int id);

        /// <summary>
        /// 查询最近一周访问记录
        /// </summary>
        /// <returns></returns>
        Task<IResultModel> QueryLatestWeekPv();

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel<ExcelExportResultModel>> Export(AuditInfoQueryModel model);
    }
}
