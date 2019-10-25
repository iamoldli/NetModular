using NetModular.Lib.Quartz.Abstractions;
using Quartz;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz.Impl;

namespace NetModular.Lib.Quartz.Core
{
    public class QuartzServer : IQuartzServer
    {
        private readonly ILogger _logger;
        private readonly NameValueCollection _props;
        private IScheduler _scheduler;
        private readonly IServiceProvider _container;

        public QuartzServer(IServiceProvider container, NameValueCollection props, ILogger<QuartzServer> logger)
        {
            _container = container;
            _props = props;
            _logger = logger;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public async Task Start(CancellationToken cancellation = default)
        {
            if (_scheduler != null)
                return;

            //调度器工厂
            var factory = _props != null ? new StdSchedulerFactory(_props) : new StdSchedulerFactory();

            //创建一个调度器
            _scheduler = await factory.GetScheduler(cancellation);

            //绑定自定义任务工厂
            _scheduler.JobFactory = new JobFactory(_container);

            //添加任务监听器
            AddJobListener();

            //添加触发器监听器
            AddTriggerListener();

            //添加调度服务监听器
            AddSchedulerListener();

            //启动
            await _scheduler.Start(cancellation);

            _logger.LogInformation("Quartz服务启动");
        }

        /// <summary>
        /// 停止
        /// </summary>
        public async Task Stop(CancellationToken cancellation = default)
        {
            if (_scheduler == null)
            {
                return;
            }

            await _scheduler.Shutdown(true, cancellation);

            _logger.LogInformation("Quartz服务停止");
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        public Task AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default)
        {
            return _scheduler.ScheduleJob(jobDetail, trigger, cancellation);
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        public Task PauseJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.PauseJob(jobKey, cancellation);
        }

        /// <summary>
        /// 恢复任务
        /// </summary>
        public Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.ResumeJob(jobKey, cancellation);
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        public Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default)
        {
            return _scheduler.DeleteJob(jobKey, cancellation);
        }

        #region ==私有方法==

        /// <summary>
        /// 添加调度服务监听器
        /// </summary>
        private void AddSchedulerListener()
        {
            var schedulerListeners = _container.GetServices<ISchedulerListener>().ToList();
            if (schedulerListeners.Any())
            {
                foreach (var listener in schedulerListeners)
                {
                    _scheduler.ListenerManager.AddSchedulerListener(listener);
                }
            }
        }

        /// <summary>
        /// 添加任务监听器
        /// </summary>
        private void AddJobListener()
        {
            var jobListeners = _container.GetServices<IJobListener>().ToList();
            if (jobListeners.Any())
            {
                foreach (var listener in jobListeners)
                {
                    _scheduler.ListenerManager.AddJobListener(listener);
                }
            }
        }

        /// <summary>
        /// 添加触发器监听器
        /// </summary>
        private void AddTriggerListener()
        {
            var triggerListener = _container.GetServices<ITriggerListener>().ToList();
            if (triggerListener.Any())
            {
                foreach (var listener in triggerListener)
                {
                    _scheduler.ListenerManager.AddTriggerListener(listener);
                }
            }
        }

        #endregion
    }
}
