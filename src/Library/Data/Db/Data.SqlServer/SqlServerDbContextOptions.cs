using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Data.Abstractions.Options;
using Tm.Lib.Data.Core;

namespace Tm.Lib.Data.SqlServer
{
    /// <summary>
    /// 数据库上下文配置项SqlServer实现
    /// </summary>
    public class SqlServerDbContextOptions : DbContextOptionsAbstract
    {
        public SqlServerDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SqlServerAdapter(options.Database), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}