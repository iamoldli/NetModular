using System.Data;
using System.Data.SQLite;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;

namespace Nm.Lib.Data.SQLite
{
    /// <summary>
    /// SQLite数据库上下文配置项
    /// </summary>
    public class SQLiteDbContextOptions : DbContextOptionsAbstract
    {
        public SQLiteDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SQLiteAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
            options.Version = dbOptions.Version;
            options.ConnectionString = $"Data Source={dbOptions.Server}";
        }

        public override IDbConnection NewConnection()
        {
            return new SQLiteConnection(DbModuleOptions.ConnectionString);
        }
    }
}
