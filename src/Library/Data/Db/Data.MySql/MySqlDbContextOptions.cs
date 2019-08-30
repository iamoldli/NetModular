using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;

namespace Nm.Lib.Data.MySql
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
        public MySqlDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new MySqlAdapter(options), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
