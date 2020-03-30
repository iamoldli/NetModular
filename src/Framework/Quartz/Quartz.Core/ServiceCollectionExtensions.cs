using System;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Quartz.Abstractions;
using NetModular.Lib.Utils.Core.Interfaces;

namespace NetModular.Lib.Quartz.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Quartz服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <param name="options">调度配置</param>
        /// <returns></returns>
        public static IServiceCollection AddQuartz(this IServiceCollection services, IModuleCollection modules)
        {
            var collection = new QuartzModuleCollection();

            #region ==反射注入所有模块的任务服务==

            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    var descriptor = new QuartzModuleDescriptor
                    {
                        Module = module
                    };

                    var quartzAssembly = module.AssemblyDescriptor.Quartz;
                    if (quartzAssembly != null)
                    {
                        var taskTypes = module.AssemblyDescriptor.Quartz.GetTypes().Where(m => typeof(ITask).IsAssignableFrom(m));
                        foreach (var taskType in taskTypes)
                        {
                            //排除HttpTask
                            if (!taskType.Name.Equals("HttpTask"))
                            {
                                var taskDescriptor = new QuartzTaskDescriptor
                                {
                                    Name = taskType.Name,
                                    Class = $"{taskType.FullName}, {taskType.Assembly.GetName().Name}"
                                };

                                var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(taskType, typeof(DescriptionAttribute));
                                if (descAttr != null && descAttr.Description.NotNull())
                                {
                                    taskDescriptor.Name = descAttr.Description;
                                }

                                descriptor.Tasks.Add(taskDescriptor);
                            }

                            services.AddTransient(taskType);
                        }
                    }

                    foreach (var task in descriptor.Tasks)
                    {
                        descriptor.TaskSelect.Add(new OptionResultModel
                        {
                            Label = task.Name,
                            Value = task.Class
                        });
                    }

                    collection.Add(descriptor);
                }
            }

            #endregion

            //注入调度模块集合
            services.AddSingleton<IQuartzModuleCollection>(collection);

            //注入日志
            services.TryAddTransient<ITaskLogger, TaskLogger>();

            //注入Quartz服务实例
            services.AddSingleton<IQuartzServer, QuartzServer>();

            //注入应用关闭事件
            services.AddSingleton<IAppShutdownHandler, QuartzAppShutdownHandler>();

            return services;
        }
    }
}
