using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tm.Lib.Quartz.Abstractions;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Quartz.Application.JobService.ViewModels;
using Tm.Module.Quartz.Domain.Job;
using Tm.Module.Quartz.Domain.Job.Models;
using Tm.Module.Quartz.Domain.JobLog;
using Tm.Module.Quartz.Domain.JobLog.Models;
using Quartz;

namespace Tm.Module.Quartz.Application.JobService
{
    public class JobService : IJobService
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _repository;
        private readonly IJobLogRepository _logRepository;
        private readonly ILogger _logger;
        private readonly IQuartzServer _quartzServer;

        public JobService(IMapper mapper, IJobRepository repository, ILogger<JobService> logger, IQuartzServer quartzServer, IJobLogRepository logRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
            _quartzServer = quartzServer;
            _logRepository = logRepository;
        }

        public async Task<IResultModel> Query(JobQueryModel model)
        {
            var result = new QueryResultModel<JobEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(JobAddModel model)
        {
            var entity = _mapper.Map<JobEntity>(model);
            entity.JobKey = $"{model.Group}.{model.Code}";
            entity.Status = JobStatus.Pause;//默认添加后暂停，需要手动启动
            entity.EndDate = entity.EndDate.AddDays(1);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
            }

            using (var tran = _repository.BeginTransaction())
            {
                if (await _repository.AddAsync(entity, tran))
                {
                    var result = await AddJob(entity);
                    if (!result.Successful)
                    {
                        return result;
                    }

                    tran.Commit();

                    return ResultModel.Success();
                }
            }

            return ResultModel.Failed("添加失败");
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return ResultModel.Failed("任务不存在");
            }

            var model = _mapper.Map<JobUpdateModel>(entity);
            model.EndDate = entity.EndDate.AddDays(-1);

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(JobUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
            {
                return ResultModel.Failed("任务不存在");
            }

            var oldStatus = entity.Status;
            var oldJobKey = new JobKey(entity.Name, entity.Group);

            _mapper.Map(model, entity);
            entity.JobKey = $"{model.Group}.{model.Code}";
            entity.EndDate = entity.EndDate.AddDays(1);

            if (await _repository.Exists(entity))
            {
                return ResultModel.Failed($"当前任务组{entity.Group}已存在任务编码${entity.Code}");
            }

            using (var tran = _repository.BeginTransaction())
            {
                //未运行状态修改为暂停
                if (oldStatus != JobStatus.Running)
                {
                    entity.Status = JobStatus.Pause;
                }

                //如果不是未完成的任务，需要先从调度服务中删除任务
                if (oldStatus != JobStatus.Completed)
                {
                    await _quartzServer.DeleteJob(oldJobKey);
                }

                if (await _repository.UpdateAsync(entity, tran))
                {
                    var result = await AddJob(entity, entity.Status == JobStatus.Running);
                    if (!result.Successful)
                    {
                        return result;
                    }

                    tran.Commit();
                    return ResultModel.Success();
                }
            }

            return ResultModel.Failed("添加失败");
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return ResultModel.Failed("任务不存在");
            }

            var result = await _repository.DeleteAsync(id);
            if (result)
            {
                if (entity.Status != JobStatus.Completed)
                {
                    var jobKey = new JobKey(entity.Code, entity.Group);

                    await _quartzServer.DeleteJob(jobKey);
                }

                return ResultModel.Success("已删除");
            }

            return ResultModel.Failed("删除失败");
        }

        public async Task<IResultModel> Pause(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return ResultModel.Failed("任务不存在");
            }

            if (entity.Status == JobStatus.Completed)
            {
                return ResultModel.Failed("任务已结束");
            }

            if (entity.Status != JobStatus.Pause)
            {
                entity.Status = JobStatus.Pause;
                using (var tran = _repository.BeginTransaction())
                {
                    if (await _repository.UpdateAsync(entity))
                    {
                        try
                        {
                            var jobKey = new JobKey(entity.Code, entity.Group);

                            await _quartzServer.PauseJob(jobKey);

                            tran.Commit();

                            return ResultModel.Success("已暂停");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"任务暂停失败：{ex}");
                        }
                    }
                }
            }

            return ResultModel.Failed("暂停失败");
        }

        public async Task<IResultModel> Resume(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                return ResultModel.Failed("任务不存在");
            }

            if (entity.Status != JobStatus.Running)
            {
                var oldStatus = entity.Status;
                entity.Status = JobStatus.Running;
                using (var tran = _repository.BeginTransaction())
                {
                    if (await _repository.UpdateAsync(entity))
                    {
                        try
                        {
                            //已完成的任务，重启需要重新加入到调度中
                            if (oldStatus == JobStatus.Completed)
                            {
                                if (entity.EndDate <= DateTime.Now)
                                {
                                    return ResultModel.Failed("任务时效已过期");
                                }

                                var result = await AddJob(entity, true);
                                if (!result.Successful)
                                {
                                    return result;
                                }
                            }
                            else
                            {
                                var jobKey = new JobKey(entity.Code, entity.Group);

                                await _quartzServer.ResumeJob(jobKey);
                            }

                            tran.Commit();

                            return ResultModel.Success("已启动");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"任务启动失败：{ex}");
                        }
                    }
                }
            }

            return ResultModel.Failed("启动失败");
        }

        public async Task<IResultModel> Log(JobLogQueryModel model)
        {
            var result = new QueryResultModel<JobLogEntity>
            {
                Rows = await _logRepository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="start">是否立即启动</param>
        /// <returns></returns>
        private async Task<IResultModel> AddJob(JobEntity entity, bool start = false)
        {
            var jobClassType = Type.GetType(entity.JobClass);
            if (jobClassType == null)
            {
                return ResultModel.Failed($"任务类({entity.JobClass})不存在");
            }

            var jobKey = new JobKey(entity.Code, entity.Group);
            var job = JobBuilder.Create(jobClassType).WithIdentity(jobKey).UsingJobData("id", entity.Id.ToString()).Build();
            var triggerBuilder = TriggerBuilder.Create().WithIdentity(entity.Code, entity.Group)
                .EndAt(entity.EndDate.ToUniversalTime())
                .WithDescription(entity.Name);

            //如果开始日期小于等于当前日期则立即启动
            if (entity.BeginDate <= DateTime.Now)
            {
                triggerBuilder.StartNow();
            }
            else
            {
                triggerBuilder.StartAt(entity.BeginDate.ToUniversalTime());
            }

            if (entity.TriggerType == TriggerType.Simple)
            {
                //简单任务
                triggerBuilder.WithSimpleSchedule(builder =>
                {
                    builder.WithIntervalInSeconds(entity.Interval);
                    if (entity.RepeatCount > 0)
                    {
                        builder.WithRepeatCount(entity.RepeatCount - 1);
                    }
                    else
                    {
                        builder.RepeatForever();
                    }
                });
            }
            else
            {
                if (!CronExpression.IsValidExpression(entity.Cron))
                {
                    return ResultModel.Failed("CRON表达式无效");
                }

                //CRON任务
                triggerBuilder.WithCronSchedule(entity.Cron);
            }

            var trigger = triggerBuilder.Build();
            try
            {
                await _quartzServer.AddJob(job, trigger);

                if (!start)
                {
                    //先暂停
                    await _quartzServer.PauseJob(jobKey);
                }

                return ResultModel.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError("任务调度添加任务失败" + ex);
            }

            return ResultModel.Failed();
        }
    }
}
