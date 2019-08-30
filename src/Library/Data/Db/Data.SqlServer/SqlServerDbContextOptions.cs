using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;

namespace Nm.Lib.Data.SqlServer
{
    /// <summary>
    /// 数据库上下文配置项SqlServer实现
    /// </summary>
    public class SqlServerDbContextOptions : DbContextOptionsAbstract
    {
        public SqlServerDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SqlServerAdapter(options), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}