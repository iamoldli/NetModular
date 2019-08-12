using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Tm.Lib.Auth.Abstractions;
using Tm.Lib.Data.Abstractions.Options;
using Tm.Lib.Data.Core;

namespace Tm.Lib.Data.MySql
{
    /// <summary>
    /// MySql数据库上下文配置项
    /// </summary>
    public class MySqlDbContextOptions : DbContextOptionsAbstract
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbOptions"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="loginInfo"></param>
        public MySqlDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new MySqlAdapter(options.Database), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
