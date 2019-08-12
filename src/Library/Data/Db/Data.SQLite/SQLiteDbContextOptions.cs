using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Data.Abstractions.Options;
using Tm.Lib.Data.Core;

namespace Tm.Lib.Data.SQLite
{
    /// <summary>
    /// SQLite数据库上下文配置项
    /// </summary>
    public class SQLiteDbContextOptions : DbContextOptionsAbstract
    {
        public SQLiteDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SQLiteAdapter(options.Database), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
