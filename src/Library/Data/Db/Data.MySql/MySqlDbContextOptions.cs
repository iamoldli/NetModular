using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core;

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
        public MySqlDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new MySqlAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
            Check.NotNull(dbOptions.Server, nameof(dbOptions.Server), "数据库服务器地址不能为空");
            Check.NotNull(dbOptions.UserId, nameof(dbOptions.UserId), "数据库用户名不能为空");
            Check.NotNull(dbOptions.Password, nameof(dbOptions.Password), "数据库密码不能为空");

            options.Version = dbOptions.Version;
            var port = DbOptions.Port > 0 ? DbOptions.Port : 3306;
            options.ConnectionString = $"Server={DbOptions.Server};Database={options.Database};Port={port};Uid={DbOptions.UserId};Pwd={DbOptions.Password};Allow User Variables=True;charset=utf8;SslMode=none;";
        }

        public override IDbConnection NewConnection()
        {
            return new MySqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}
