using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Infrastructure.Repositories.MySql.Sql;

namespace Nm.Module.Admin.Infrastructure.Repositories.MySql
{
    public class AuditInfoRepository : SqlServer.AuditInfoRepository
    {
        public AuditInfoRepository(IDbContext context) : base(context)
        {
        }

        public override Task<IEnumerable<ChatDataRow>> QueryLatestWeekPv()
        {
            var sql = string.Format(AuditInfoSql.QueryLatestWeekPv, Db.EntityDescriptor.TableName);
            return Db.QueryAsync<ChatDataRow>(sql);
        }
    }
}
