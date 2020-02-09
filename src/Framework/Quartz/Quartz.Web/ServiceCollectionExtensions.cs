using System.Collections.Specialized;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Quartz.Abstractions;
using NetModular.Lib.Quartz.Core;
using NetModular.Lib.Utils.Core.Interfaces;

namespace NetModular.Lib.Quartz.Web
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <param name="props">属性</param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, IModuleCollection modules, NameValueCollection props)
        {
            #region ==反射注入所有模块的任务服务==

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

            //注入Quartz服务实例
            services.AddSingleton<IQuartzServer>(sp => new QuartzServer(props, sp));

            //注入应用关闭事件
            services.AddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            return services;
        }
    }
}
