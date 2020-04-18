using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Config.Core;
using NetModular.Lib.Data.Integration;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.GenericHost;
using NetModular.Lib.Utils.Core;

namespace NetModular.Lib.Host.Generic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericHost(this IServiceCollection services, IHostEnvironment env, Action<IServiceCollection, IHostEnvironment> configureServices = null)
        {
            //添加所有通过特性注入的服务
            services.AddNetModularServices();

            //加载模块
            var modules = services.AddModules();

            //添加对象映射
            services.AddMappers(modules);

            //添加数据库
            services.AddDb(env.EnvironmentName, modules);

            //添加HttpClient相关
            services.AddHttpClient();

            //添加模块的自定义服务
            services.AddModuleServices(modules, env);

            //添加配置管理
            services.AddConfig();

            //自定义服务
            configureServices?.Invoke(services, env);

            return services;
        }
    }
}
