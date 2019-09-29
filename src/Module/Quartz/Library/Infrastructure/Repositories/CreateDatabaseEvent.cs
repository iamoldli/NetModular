using System.Threading.Tasks;
using Dapper;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Enums;

namespace Nm.Module.Quartz.Infrastructure.Repositories
{
    public class CreateDatabaseEvent : IDatabaseCreateEvents
    {
        public IDbContext DbContext { get; set; }

        public Task Before()
        {
            return Task.CompletedTask;
        }


        public async Task After()
        {
            var sql = new CreateTableSql();

            using var con = DbContext.NewConnection();
            if (DbContext.Options.DbOptions.Dialect == SqlDialect.SqlServer)
            {
                var exist = con.ExecuteScalar<int>("SELECT TOP 1 1 FROM sysobjects WHERE id = OBJECT_ID(N'QRTZ_BLOB_TRIGGERS') AND xtype = 'U';") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.SqlServer)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
            }
            else if (DbContext.Options.DbOptions.Dialect == SqlDialect.MySql)
            {
                var exist = con.ExecuteScalar<int>($"SELECT 1 FROM information_schema.TABLES WHERE table_schema = '{DbContext.Options.DbModuleOptions.Database}' AND table_name = 'qrtz_blob_triggers' limit 1;") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.MySql)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
            }
            else if (DbContext.Options.DbOptions.Dialect == SqlDialect.SQLite)
            {
                var exist = con.ExecuteScalar<int>("SELECT 1 FROM sqlite_master WHERE type = 'table' and name='QRTZ_BLOB_TRIGGERS';") > 0;
                if (!exist)
                {
                    foreach (var cmd in sql.SQLite)
                    {
                        await con.ExecuteAsync(cmd);
                    }
                }
            }
        }
    }
}
