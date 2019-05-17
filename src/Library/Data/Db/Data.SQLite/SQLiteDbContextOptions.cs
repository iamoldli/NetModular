using System.Data;
using System.Data.SQLite;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;

namespace Nm.Lib.Data.SQLite
{
    /// <summary>
    /// SQLite数据库上下文配置项
    /// </summary>
    public class SQLiteDbContextOptions : DbContextOptionsAbstract
    {
        public SQLiteDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor) : base(dbOptions, options, new SQLiteAdapter(options.Database), loggerFactory, httpContextAccessor)
        {
        }

        public override IDbConnection OpenConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
