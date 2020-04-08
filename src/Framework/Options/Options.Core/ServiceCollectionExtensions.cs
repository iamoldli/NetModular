using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Options.Abstraction;

namespace NetModular.Lib.Options.Core
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 添加模块配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        public static IServiceCollection AddModuleOptions(this IServiceCollection services, IModuleCollection modules)
        {
            //如果不包含IModuleOptionsStorageProvider的实现，则不添加配置功能
            if (services.All(m => m.ServiceType != typeof(IModuleOptionsStorageProvider)))
            {
                return services;
            }

            var collection = new ModuleOptionsCollection();

            if (modules != null && modules.Any())
            {
                foreach (var module in modules)
                {
                    var descriptor = GetDescriptor(services, module);

                    collection.Add(descriptor);
                }
            }

            //注入变更事件
            services.AddChangedEvent(collection);

            //注入配置集合
            services.AddSingleton<IModuleOptionsCollection>(collection);

            //注入引擎
            services.AddSingleton<IModuleOptionsEngine, ModuleOptionsEngine>();

            //从存储加载数据
            services.BuildServiceProvider().GetService<IModuleOptionsEngine>().LoadFromStorage(services);

            return services;
        }

        /// <summary>
        /// 注入配置对象
        /// </summary>
        private static ModuleOptionsDescriptor GetDescriptor(IServiceCollection services, IModuleDescriptor module)
        {
            var descriptor = new ModuleOptionsDescriptor
            {
                Module = module
            };

            //一个模块中只能有一个配置项，且必须按照“模块名称+Options”的命名方式
            var optionsType = module.AssemblyDescriptor.Infrastructure.GetTypes()
                .FirstOrDefault(m => typeof(IModuleOptions).IsAssignableFrom(m) && m.Name.EqualsIgnoreCase($"{module.Code}Options"));

            if (optionsType != null)
            {
                descriptor.OptionsType = optionsType;

                var properties = optionsType.GetProperties().Where(m => m.GetCustomAttributes(false).Any(n => n.GetType() == typeof(ModuleOptionDefinitionAttribute)));
                foreach (var p in properties)
                {
                    descriptor.Definitions.Add(GetDefinition(optionsType, p));
                }

                //这里注入是为了能够通过依赖注入获取模块配置实例，这样模块配置就可以使用以来注入来注入任意服务了
                //在IModuleOptionsEngine的LoadFromStorage方法中会删除并重新注入
                services.AddSingleton(optionsType);
            }

            return descriptor;
        }

        /// <summary>
        /// 注入变更事件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="collection"></param>
        private static void AddChangedEvent(this IServiceCollection services, ModuleOptionsCollection collection)
        {
            foreach (var descriptor in collection)
            {
                var changedEventTypes = descriptor.Module.AssemblyDescriptor.Infrastructure.GetTypes().Where(m => typeof(IModuleOptionsChangedEvent<>).IsImplementType(m)).ToList();

                if (!changedEventTypes.Any())
                    continue;

                foreach (var eventType in changedEventTypes)
                {
                    //将变更事件保存下来，方便配置更改时从容器中查找
                    var optionType = eventType.GetInterfaces().First().GetGenericArguments()[0];
                    collection.First(m => m.OptionsType == optionType).ChangedEvents.Add(eventType);

                    services.AddSingleton(eventType);
                }
            }
        }

        /// <summary>
        /// 获取配置属性定义信息
        /// </summary>
        /// <param name="optionsType"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        private static ModuleOptionDefinitionAttribute GetDefinition(Type optionsType, PropertyInfo property)
        {
            var definition = (ModuleOptionDefinitionAttribute)property.GetCustomAttributes(false).FirstOrDefault(m => m.GetType() == typeof(ModuleOptionDefinitionAttribute));
            if (definition != null)
            {
                definition.Key = optionsType.FullName + "." + property.Name;
                definition.DataName = property.Name.FirstCharToLower();
                definition.PropertyInfo = property;

                if (definition.DataType == ModuleOptionDataType.UnKnown)
                {
                    var propertyType = property.PropertyType;
                    if (propertyType.IsNullable())
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);

                        if (propertyType == null)
                            throw new Exception("Module options property type is null");
                    }

                    //默认字符串类型
                    definition.DataType = ModuleOptionDataType.String;

                    #region ==枚举类型==

                    if (propertyType.IsEnum)
                    {
                        definition.DataType = ModuleOptionDataType.Enum;
                        definition.EnumOptions = Enum.GetValues(propertyType).Cast<Enum>()
                              .Where(m => !m.ToString().Equals("UnKnown")).Select(x => new OptionResultModel
                              {
                                  Label = x.ToDescription(),
                                  Value = x
                              }).ToList();
                    }

                    #endregion

                    #region ==整数类型==

                    else if (propertyType.IsByte() || propertyType.IsChar() || propertyType.IsShort() || propertyType.IsInt() || propertyType.IsLong())
                    {
                        definition.DataType = ModuleOptionDataType.Int;
                    }

                    #endregion

                    #region ==小数类型==

                    else if (propertyType.IsFloat() || propertyType.IsDouble() || propertyType.IsDecimal())
                    {
                        definition.DataType = ModuleOptionDataType.Decimal;
                    }

                    #endregion

                    #region ==日期类型==

                    else if (propertyType.IsDateTime())
                    {
                        definition.DataType = ModuleOptionDataType.Date;
                    }

                    #endregion

                    #region ==布尔类型==

                    else if (propertyType.IsBool())
                    {
                        definition.DataType = ModuleOptionDataType.Bool;
                    }

                    #endregion
                }
            }

            return definition;
        }
    }
}
