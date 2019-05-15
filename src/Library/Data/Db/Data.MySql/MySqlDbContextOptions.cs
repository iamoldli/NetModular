using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
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
        /// <param name="httpContextAccessor"></param>
        public MySqlDbContextOptions(DbOptions dbOptions, DbConnectionOptions options, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor) : base(dbOptions, options, new MySqlAdapter(options.Database), loggerFactory, httpContextAccessor)
        {
        }

        public override IDbConnection OpenConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
