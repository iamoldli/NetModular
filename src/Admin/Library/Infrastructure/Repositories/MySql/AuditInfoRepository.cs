using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Infrastructure.Repositories.MySql.Sql;

namespace NetModular.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AuditInfoRepository : SqlServer.AuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<ChartDataResultModel>> QueryLatestWeekPv()
        {
            var sql = string.Format(AuditInfoSql.QueryLatestWeekPv, Db.EntityDescriptor.TableName);
            return Db.QueryAsync<ChartDataResultModel>(sql);
        }
    }
}
