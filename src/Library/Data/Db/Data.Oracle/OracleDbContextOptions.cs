using System.Data;
using Microsoft.Extensions.Logging;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Data.Abstractions.Options;
using Tm.Lib.Data.Core;
using Oracle.ManagedDataAccess.Client;

namespace Tm.Lib.Data.Oracle
{
    /// <summary>
    /// Oracle数据库上下文配置项
    /// </summary>
    public class OracleDbContextOptions : DbContextOptionsAbstract
    {
        public OracleDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new OracleAdapter(options.Database), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new OracleConnection(ConnectionString);
        }
    }
}
