using System.Data;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
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
        public MySqlDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new MySqlAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
            if (options.ConnectionString.IsNull())
            {
                Check.NotNull(dbOptions.Server, nameof(dbOptions.Server), "数据库服务器地址不能为空");
                Check.NotNull(dbOptions.UserId, nameof(dbOptions.UserId), "数据库用户名不能为空");
                Check.NotNull(dbOptions.Password, nameof(dbOptions.Password), "数据库密码不能为空");

                options.Version = dbOptions.Version;
                var connStrBuilder = new MySqlConnectionStringBuilder
                {
                    Server = DbOptions.Server,
                    Port = DbOptions.Port > 0 ? (uint)DbOptions.Port : 3306,
                    Database = options.Database,
                    UserID = DbOptions.UserId,
                    Password = DbOptions.Password,
                    AllowUserVariables = true,
                    CharacterSet = "utf8",
                    SslMode = MySqlSslMode.None,
                    AllowPublicKeyRetrieval = true,
                    MinimumPoolSize = dbOptions.MinPoolSize < 1 ? 0u : dbOptions.MinPoolSize.ToByte(),
                    MaximumPoolSize = dbOptions.MaxPoolSize < 1 ? 10u : dbOptions.MaxPoolSize.ToByte()
                };
                options.ConnectionString = connStrBuilder.ToString();
            }
        }

        public override IDbConnection NewConnection()
        {
            return new MySqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}
