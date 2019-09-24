using System;
using System.Data;
using Microsoft.Extensions.Logging;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;

namespace Nm.Lib.Data.Oracle
{
    /// <summary>
    /// Oracle数据库上下文配置项
    /// </summary>
    public class OracleDbContextOptions : DbContextOptionsAbstract
    {
        public OracleDbContextOptions(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory, ILoginInfo loginInfo) : base(dbOptions, options, new OracleAdapter(dbOptions, options), loggerFactory, loginInfo)
        {
        }

        public override IDbConnection NewConnection()
        {
            throw new Exception();
        }
    }
}
