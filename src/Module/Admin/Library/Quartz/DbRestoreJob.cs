using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Nm.Lib.Quartz.Abstractions;
using Nm.Lib.Quartz.Core;
using Nm.Module.Admin.Infrastructure.Repositories;

namespace Nm.Module.Admin.Quartz
{
    [Description("定时还原Admin数据库任务")]
    public class DbRestoreJob : JobTaskAbstract
    {
        private readonly AdminDbContext _dbContext;
        public DbRestoreJob(IJobLogger logger, AdminDbContext dbContext) : base(logger)
        {
            _dbContext = dbContext;
        }

        public override async Task Execute(IJobTaskContext context)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "nm_admin.sql");
            if (!File.Exists(path))
            {
                await Logger.Error("Sql文件不存在~");

                return;
            }

            using (var sr = new StreamReader(path))
            {
                var sql = await sr.ReadToEndAsync();

                using (var con = _dbContext.NewConnection())
                {
                    await con.ExecuteAsync(sql);
                    await Logger.Info("数据库已还原");
                }
            }
        }
    }
}
