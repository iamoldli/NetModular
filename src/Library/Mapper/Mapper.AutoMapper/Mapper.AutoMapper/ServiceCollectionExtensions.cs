using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Mapper.AutoMapper
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加对象映射
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules">模块集合</param>
        /// <returns></returns>
        public static IServiceCollection AddMappers(this IServiceCollection services, IModuleCollection modules)
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var moduleInfo in modules)
                {
                    var types = moduleInfo.AssembliesInfo.Application.GetTypes().Where(t => typeof(IMapperConfig).IsAssignableFrom(t));

                    foreach (var type in types)
                    {
                        ((IMapperConfig)Activator.CreateInstance(type)).Bind(cfg);
                    }
                }
            });

            services.AddSingleton(config.CreateMapper());

            return services;
        }
    }
}
