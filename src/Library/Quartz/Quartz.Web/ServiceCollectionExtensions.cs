using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Quartz.Abstractions;
using Nm.Lib.Quartz.Core;

namespace Nm.Lib.Quartz.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="props">属性</param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, NameValueCollection props)
        {
            #region ==反射注入所有模块的任务服务==

            var modules = services.BuildServiceProvider().GetService<IModuleCollection>();
            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    var quartzAssembly = module.AssemblyDescriptor.Quartz;
                    if (quartzAssembly != null)
                    {
                        var jobTypes = quartzAssembly.GetTypes().Where(m => typeof(IJobTask).IsAssignableFrom(m));
                        foreach (var jobType in jobTypes)
                        {
                            services.AddTransient(jobType);
                        }
                    }
                }
            }

            #endregion

            //注入日志
            services.TryAddTransient<IJobLogger, JobLogger>();

            var sp = services.BuildServiceProvider();
            var logger = sp.GetService<ILogger<QuartzServer>>();
            var server = new QuartzServer(sp, props, logger);
            server.Start().Wait();

            //注入Quartz服务
            services.AddSingleton<IQuartzServer>(server);

            return services;
        }
    }
}
