using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Lib.Data.SqlServer
{
    /// <summary>
    /// 数据库上下文配置项SqlServer实现
    /// </summary>
    public class SqlServerDbContextOptions : DbContextOptionsAbstract
    {
        public SqlServerDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new SqlServerAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
            if (options.ConnectionString.IsNull())
            {
                Check.NotNull(dbOptions.Server, nameof(dbOptions.Server), "数据库服务器地址不能为空");
                Check.NotNull(dbOptions.UserId, nameof(dbOptions.UserId), "数据库用户名不能为空");
                Check.NotNull(dbOptions.Password, nameof(dbOptions.Password), "数据库密码不能为空");

                options.Version = DbOptions.Version;
                var connStrBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = DbOptions.Port > 0 ? DbOptions.Server + "," + DbOptions.Port : DbOptions.Server,
                    UserID = DbOptions.UserId,
                    Password = DbOptions.Password,
                    MultipleActiveResultSets = true,
                    InitialCatalog = DbModuleOptions.Database
                };
                options.ConnectionString = connStrBuilder.ToString();
            }
        }

        public override IDbConnection NewConnection()
        {
            return new SqlConnection(DbModuleOptions.ConnectionString);
        }
    }
}