using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Tm.Lib.Module.Abstractions;

namespace Tm.Lib.Mapper.AutoMapper
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
                    var types = moduleInfo.AssemblyDescriptor.Application.GetTypes().Where(t => typeof(IMapperConfig).IsAssignableFrom(t));

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
