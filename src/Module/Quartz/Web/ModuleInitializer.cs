using System.Collections.Specialized;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Module.AspNetCore;
using Nm.Module.Quartz.Infrastructure.Jobs;
using Quartz;
using Quartz.Impl;

namespace Nm.Module.Quartz.Web
{
    public class ModuleInitializer : IModuleInitializer
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
        }

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            

            //调度器工厂
            var factory = new StdSchedulerFactory(new NameValueCollection());

            //创建一个调度器
            var scheduler = factory.GetScheduler().GetAwaiter().GetResult();

            var jobKey = new JobKey("timer", "group");
            var job = JobBuilder.Create<TimerJob>().WithIdentity(jobKey).Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "group")
                .WithCronSchedule("0/5 * * * * ?") //5秒执行一次
                .Build();

            scheduler.ScheduleJob(job, trigger).GetAwaiter().GetResult();

            scheduler.Start().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 配置MVC功能
        /// </summary>
        /// <param name="mvcOptions"></param>
        public void ConfigureMvc(MvcOptions mvcOptions)
        {

        }
    }
}
