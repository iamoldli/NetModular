using System.Threading.Tasks;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Domain.AuditInfo;
using Nm.Module.Admin.Domain.AuditInfo.Models;

namespace Nm.Module.Admin.Application.AuditInfoService
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
    }
}
