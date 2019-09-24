using System.Threading.Tasks;
using Dapper;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Enums;

namespace Nm.Module.Quartz.Infrastructure.Repositories
{
    public class CreateDatabaseEvent : ICreateDatabaseEvent
    {
        public Task Before(IDbContext dbContext)
        {
            return Task.CompletedTask;
        }


        public async Task After(IDbContext dbContext)
        {
            var sql = new CreateTableSql();

            using (var con = dbContext.NewConnection())
            {
                if (dbContext.Options.DbOptions.Dialect == SqlDialect.SqlServer)
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
                else if (dbContext.Options.DbOptions.Dialect == SqlDialect.MySql)
                {
                    var exist = con.ExecuteScalar<int>($"SELECT 1 FROM information_schema.TABLES WHERE table_schema = '{dbContext.Options.DbModuleOptions.Database}' AND table_name = 'qrtz_blob_triggers' limit 1;") > 0;
                    if (!exist)
                    {
                        foreach (var cmd in sql.MySql)
                        {
                            await con.ExecuteAsync(cmd);
                        }
                    }
                }
            }
        }
    }
}
