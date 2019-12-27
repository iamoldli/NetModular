using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Cache.Integration;
using NetModular.Lib.Data.Integration;
using NetModular.Lib.Excel.Integration;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.GenericHost;
using NetModular.Lib.Utils.Core;
using NetModular.Lib.Utils.Core.Options;

namespace NetModular.Lib.Host.Generic
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericHost(this IServiceCollection services, IHostEnvironment env, Action<IServiceCollection, IHostEnvironment> configureServices = null)
        {
            services.AddUtils();

            //加载模块
            var modules = services.AddModules(env.EnvironmentName, out ModuleCommonOptions moduleCommonOptions);

            //添加对象映射
            services.AddMappers(modules);

            //添加缓存
            services.AddCache(env.EnvironmentName);

            //添加Excel相关功能
            services.AddExcel(env.EnvironmentName, moduleCommonOptions);

            //添加数据库
            services.AddDb(env.EnvironmentName, modules);

            //自定义服务
            configureServices?.Invoke(services, env);

            //添加HttpClient相关
            services.AddHttpClient();

            return services;
        }
    }
}
