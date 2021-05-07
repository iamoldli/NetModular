using System.Data;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;

namespace NetModular.Lib.Data.MySql
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
        public MySqlDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new MySqlAdapter(dbOptions, options, loggerFactory), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            return new MySqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}
