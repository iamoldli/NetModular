using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Options.Core
{
    public class ModuleOptionsContainer : IModuleOptionsContainer
    {
        /// <summary>
        /// 模块配置项定义信息列表
        /// </summary>
        public static readonly Dictionary<string, List<ModuleOptionDefinitionAttribute>> ModuleOptionsDefinitionList = new Dictionary<string, List<ModuleOptionDefinitionAttribute>>();
        /// <summary>
        /// 所有配置定义信息集合
        /// </summary>
        public static readonly Dictionary<string, ModuleOptionDefinitionAttribute> AllDefinitionList = new Dictionary<string, ModuleOptionDefinitionAttribute>();
        private readonly ILogger _logger;
        private readonly IModuleCollection _moduleCollection;
        private readonly IModuleOptionsStorageProvider _storageProvider;
        private readonly IServiceProvider _serviceProvider;

        public ModuleOptionsContainer(ILogger<ModuleOptionsContainer> logger, IModuleCollection moduleCollection, IModuleOptionsStorageProvider storageProvider, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _moduleCollection = moduleCollection;
            _storageProvider = storageProvider;
            _serviceProvider = serviceProvider;
        }

        public void Load(IServiceCollection services)
        {
            if (_moduleCollection != null)
            {
                _logger.LogInformation("加载所有配置项");

                //查询所有
                var list = _storageProvider.GetAll().GetAwaiter().GetResult();

                foreach (var module in _moduleCollection)
                {
                    _logger.LogInformation("刷新配置项：{@name}", module.Name);

                    //一个模块中只能有一个配置项，且必须按照“模块名称+Options”的命名方式
                    var optionsType = module.AssemblyDescriptor.Infrastructure.GetTypes()
                        .FirstOrDefault(m => typeof(IModuleOptions).IsAssignableFrom(m) && m.Name.EqualsIgnoreCase($"{module.Id}Options"));

                    if (optionsType != null)
                    {
                        var properties = optionsType.GetProperties().Where(m => m.GetCustomAttributes(false).Any(n => n.GetType() == typeof(ModuleOptionDefinitionAttribute)));
                        var options = (IModuleOptions)Activator.CreateInstance(optionsType);

                        //配置项定义信息列表
                        var definitionList = new List<ModuleOptionDefinitionAttribute>();

                        foreach (var property in properties)
                        {
                            var key = optionsType.FullName + "." + property.Name;
                            var val = list.FirstOrDefault(m => m.Key.EqualsIgnoreCase(key));
                            if (val != null)
                            {
                                try
                                {
                                    property.SetValue(options, Convert.ChangeType(val.Value, property.PropertyType));
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError("加载模块配置错误：{@ex}", ex);
                                }
                            }

                            var definition = GetDefinition(property);
                            if (definition != null)
                            {
                                definitionList.Add(definition);

                                AllDefinitionList.Add(key, definition);
                            }
                        }

                        ModuleOptionsDefinitionList.Add(module.Id, definitionList);

                        services.AddSingleton(options);
                        services.AddSingleton(optionsType, options);
                    }
                }
            }
        }

        public List<ModuleOptionDefinitionAttribute> GetDefinitions(string moduleCode)
        {
            if (moduleCode.IsNull())
                return null;

            return ModuleOptionsDefinitionList.FirstOrDefault(m => m.Key.EqualsIgnoreCase(moduleCode)).Value;
        }

        public IModuleOptions GetInstance(string moduleCode)
        {
            if (moduleCode.IsNull())
                return default;

            return _serviceProvider.GetServices<IModuleOptions>().FirstOrDefault(m => m.GetType().Name.ToLower().StartsWith(moduleCode.ToLower()));
        }

        public void RefreshInstance(string moduleCode, Dictionary<string, object> values)
        {
            if (values == null || !values.Any())
                return;

            var instance = GetInstance(moduleCode);
            if (instance == null)
                return;

            var instanceType = instance.GetType();
            var properties = instanceType.GetProperties();
            var descriptors = new List<ModuleOptionDescriptor>();
            foreach (var p in properties)
            {
                var value = values.FirstOrDefault(m => m.Key.EqualsIgnoreCase(p.Name)).Value;

                #region ==设置属性值==

                if (p.PropertyType.IsEnum)
                {
                    p.SetValue(instance, Enum.Parse(p.PropertyType, value.ToString()));
                }
                else if (p.PropertyType.IsNullable() && value == null)
                {
                    p.SetValue(instance, null);
                }
                else
                {
                    var pType = p.PropertyType;
                    if (pType.IsNullable())
                    {
                        pType = Nullable.GetUnderlyingType(pType);
                    }

                    if (pType.IsString())
                    {
                        p.SetValue(instance, value.ToString());
                    }
                    else if (pType.IsDateTime() || pType.IsBool())
                    {
                        p.SetValue(instance, value);
                    }
                    else if (pType.IsGuid())
                    {
                        p.SetValue(instance, Guid.Parse(value.ToString()));
                    }
                    else
                    {
                        p.SetValue(instance, Convert.ChangeType(value, pType));
                    }
                }

                #endregion

                var descriptor = new ModuleOptionDescriptor
                {
                    Key = $"{instanceType.FullName}.{p.Name}",
                    Value = value?.ToString()
                };
                var definition = AllDefinitionList.FirstOrDefault(m => m.Key == descriptor.Key);
                descriptor.Remarks = definition.Value.Name;
                descriptors.Add(descriptor);
            }

            //持久化
            _storageProvider.Save(descriptors).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 获取配置项定义信息
        /// </summary>
        /// <param name="property"></param>
        private ModuleOptionDefinitionAttribute GetDefinition(PropertyInfo property)
        {
            var definition = (ModuleOptionDefinitionAttribute)property.GetCustomAttributes(false).FirstOrDefault(m => m.GetType() == typeof(ModuleOptionDefinitionAttribute));
            if (definition != null)
            {
                definition.PropertyName = property.Name.FirstCharToLower();

                if (definition.PropertyType == ModuleOptionPropertyType.UnKnown)
                {
                    var propertyType = property.PropertyType;
                    if (propertyType.IsNullable())
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);

                        if (propertyType == null)
                            throw new Exception("Module options property type is null");
                    }

                    //默认字符串类型
                    definition.PropertyType = ModuleOptionPropertyType.String;

                    #region ==枚举类型==

                    if (propertyType.IsEnum)
                    {
                        definition.PropertyType = ModuleOptionPropertyType.Enum;
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
                        definition.PropertyType = ModuleOptionPropertyType.Int;
                    }

                    #endregion

                    #region ==小数类型==

                    else if (propertyType.IsFloat() || propertyType.IsDouble() || propertyType.IsDecimal())
                    {
                        definition.PropertyType = ModuleOptionPropertyType.Decimal;
                    }

                    #endregion

                    #region ==日期类型==

                    else if (propertyType.IsDateTime())
                    {
                        definition.PropertyType = ModuleOptionPropertyType.Date;
                    }

                    #endregion

                    #region ==布尔类型==

                    else if (propertyType.IsBool())
                    {
                        definition.PropertyType = ModuleOptionPropertyType.Bool;
                    }

                    #endregion
                }

            }

            return definition;
        }
    }
}
