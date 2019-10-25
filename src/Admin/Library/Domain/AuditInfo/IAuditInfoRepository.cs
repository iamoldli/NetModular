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
        Task<IList<AuditInfoEntity>> Query(AuditInfoQueryModel model);

        Task<AuditInfoEntity> Details(int id);

        /// <summary>
        /// 查询最近一周访问量
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ChatDataRow>> QueryLatestWeekPv();
    }
}
