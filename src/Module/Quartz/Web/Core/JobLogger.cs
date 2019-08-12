using System;
using System.Threading.Tasks;
using Tm.Lib.Quartz.Abstractions;
using Tm.Module.Quartz.Domain.JobLog;

namespace Tm.Module.Quartz.Web.Core
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class JobLogger : IJobLogger
    {
        private readonly IJobLogRepository _repository;

        public JobLogger(IJobLogRepository repository)
        {
            _repository = repository;
        }

        public Guid JobId { get; set; }

        public async Task Info(string msg)
        {
            var entity = new JobLogEntity
            {
                JobId = JobId,
                Type = JobLogType.Info,
                Msg = msg,
                CreatedTime = DateTime.Now
            };

            await _repository.AddAsync(entity);
        }

        public async Task Debug(string msg)
        {
            var entity = new JobLogEntity
            {
                JobId = JobId,
                Type = JobLogType.Debug,
                Msg = msg,
                CreatedTime = DateTime.Now
            };

            await _repository.AddAsync(entity);
        }

        public async Task Error(string msg)
        {
            var entity = new JobLogEntity
            {
                JobId = JobId,
                Type = JobLogType.Error,
                Msg = msg,
                CreatedTime = DateTime.Now
            };

            await _repository.AddAsync(entity);
        }
    }
}
