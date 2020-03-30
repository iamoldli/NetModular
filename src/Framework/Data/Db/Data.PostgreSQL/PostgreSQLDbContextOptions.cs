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
        public PostgreSQLDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new PostgreSQLAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
            if (options.ConnectionString.IsNull())
            {
                Check.NotNull(dbOptions.Server, nameof(dbOptions.Server), "数据库服务器地址不能为空");
                Check.NotNull(dbOptions.UserId, nameof(dbOptions.UserId), "数据库用户名不能为空");
                Check.NotNull(dbOptions.Password, nameof(dbOptions.Password), "数据库密码不能为空");

                options.Version = dbOptions.Version;
                var connStrBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = DbOptions.Server,
                    Port = DbOptions.Port > 0 ? DbOptions.Port : 5432,
                    Database = "postgres",
                    Username = DbOptions.UserId,
                    Password = DbOptions.Password
                };
                if (dbOptions.NpgsqlDatabaseName.NotNull())
                {
                    connStrBuilder.Database = dbOptions.NpgsqlDatabaseName;
                }
                options.ConnectionString = connStrBuilder.ToString();
            }
        }

        public override IDbConnection NewConnection()
        {
            return new NpgsqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}
