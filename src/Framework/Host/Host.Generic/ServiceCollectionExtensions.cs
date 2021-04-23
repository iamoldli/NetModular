using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Cache.Integration;
using NetModular.Lib.Config.Core;
using NetModular.Lib.Data.Integration;
using NetModular.Lib.Excel.Integration;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.GenericHost;
using NetModular.Lib.OSS.Integration;
using NetModular.Lib.Utils.Core;
using System;

namespace NetModular.Lib.Host.Generic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericHost(this IServiceCollection services, IConfiguration cfg, IHostEnvironment env, Action<IServiceCollection, IHostEnvironment, IConfiguration> configureServices = null)
        {
            //添加所有通过特性注入的服务
            services.AddNetModularServices();

            //加载缓存
            services.AddCache(cfg);

            //加载模块
            var modules = services.AddModules();

            //添加对象映射
            services.AddMappers(modules);

            //添加数据库
            services.AddDb(cfg, modules);

            //添加HttpClient相关
            services.AddHttpClient();

            //添加模块的自定义服务
            services.AddModuleServices(modules, env, cfg);

            //添加配置管理
            services.AddConfig();

            //自定义服务
            configureServices?.Invoke(services, env, cfg);

            //添加实体观察者处理器
            services.AddEntityObserversHandler(modules);

            //添加Excel相关功能
            services.AddExcel(cfg);

            //添加OSS相关功能
            services.AddOSS(cfg);

            return services;
        }
    }
}
