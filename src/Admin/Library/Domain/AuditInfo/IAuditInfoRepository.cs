using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.AuditInfo.Models;

namespace NetModular.Module.Admin.Domain.AuditInfo
{
    /// <summary>
    /// 审计信息仓储
    /// </summary>
    public interface IAuditInfoRepository : IRepository<AuditInfoEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model);

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AuditInfoEntity> Details(int id);

        /// <summary>
        /// 查询最近一周访问量
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPv();
    }
}
