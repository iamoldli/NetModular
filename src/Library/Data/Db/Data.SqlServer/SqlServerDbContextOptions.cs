using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;

namespace NetModular.Lib.Data.SqlServer
{
    /// <summary>
    /// 数据库上下文配置项SqlServer实现
    /// </summary>
    public class SqlServerDbContextOptions : DbContextOptionsAbstract
    {
        public SqlServerDbContextOptions(DbConnectionOptions options, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor) : base(options, new SqlServerAdapter(options.Database), loggerFactory, httpContextAccessor)
        {
        }

        public override IDbConnection OpenConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}