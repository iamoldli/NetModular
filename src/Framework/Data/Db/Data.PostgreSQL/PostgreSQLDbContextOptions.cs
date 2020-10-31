using System.Data;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using Npgsql;

namespace NetModular.Lib.Data.PostgreSQL
{
    /// <summary>
    /// MySql数据库上下文配置项
    /// </summary>
    public class PostgreSQLDbContextOptions : DbContextOptionsAbstract
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbOptions"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="loginInfo"></param>
        public PostgreSQLDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new PostgreSQLAdapter(dbOptions, options, loggerFactory), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new NpgsqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}
