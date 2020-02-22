using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Extensions;
using Newtonsoft.Json;

namespace NetModular.Lib.Options.Core
{
    public class ModuleOptionsEngine : IModuleOptionsEngine
    {
        private readonly ILogger _logger;
        private readonly IModuleOptionsStorageProvider _storageProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IModuleOptionsCollection _moduleOptionsCollection;

        public ModuleOptionsEngine(ILogger<ModuleOptionsEngine> logger, IModuleOptionsStorageProvider storageProvider, IServiceProvider serviceProvider, IModuleOptionsCollection moduleOptionsCollection)
        {
            _logger = logger;
            _storageProvider = storageProvider;
            _serviceProvider = serviceProvider;
            _moduleOptionsCollection = moduleOptionsCollection;
        }

        public void LoadFromStorage(IServiceCollection services)
        {
            //从持久化存储中查询所有配置信息
            var list = _storageProvider.GetAll();
            foreach (var descriptor in _moduleOptionsCollection)
            {
                var options = (IModuleOptions)_serviceProvider.GetService(descriptor.OptionsType);
                foreach (var definition in descriptor.Definitions)
                {
                    var val = list.FirstOrDefault(m => m.Key.EqualsIgnoreCase(definition.Key));
                    if (val != null)
                    {
                        try
                        {
                            var property = definition.PropertyInfo;
                            property.SetValue(options, Convert.ChangeType(val.Value, property.PropertyType));
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("加载模块配置错误：{@ex}", ex);
                        }
                    }
                }

                services.Remove(services.FirstOrDefault(m => m.ServiceType == descriptor.OptionsType));

                services.AddSingleton(descriptor.OptionsType, options);
            }
        }

        public List<ModuleOptionDefinitionAttribute> GetDefinitions(string moduleCode)
        {
            if (moduleCode.IsNull())
                return null;

            var descriptor = _moduleOptionsCollection.FirstOrDefault(m => m.Module.Id.EqualsIgnoreCase(moduleCode));
            return descriptor?.Definitions;
        }

        public IModuleOptions GetInstance(string moduleCode)
        {
            if (moduleCode.IsNull())
                return null;

            var descriptor = _moduleOptionsCollection.FirstOrDefault(m => m.Module.Id.EqualsIgnoreCase(moduleCode));
            if (descriptor != null && descriptor.OptionsType != null)
            {
                return (IModuleOptions)_serviceProvider.GetService(descriptor.OptionsType);
            }

            return null;
        }

        public void RefreshInstance(string moduleCode, Dictionary<string, object> values)
        {
            if (values == null || !values.Any())
                return;

            var descriptor = _moduleOptionsCollection.FirstOrDefault(m => m.Module.Id.EqualsIgnoreCase(moduleCode));
            if (descriptor == null || descriptor.OptionsType == null)
            {
                return;
            }

            var instance = (IModuleOptions)_serviceProvider.GetService(descriptor.OptionsType);
            if (instance == null)
                return;

            //通过序列化深拷贝保留旧实例
            var oldInstance = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(instance), descriptor.OptionsType);

            var storageModels = new List<ModuleOptionStorageModel>();
            foreach (var definition in descriptor.Definitions)
            {
                var value = values.FirstOrDefault(m => m.Key.EqualsIgnoreCase(definition.DataName)).Value;

                var propertyInfo = definition.PropertyInfo;

                #region ==设置属性值==

                if (propertyInfo.PropertyType.IsEnum)
                {
                    propertyInfo.SetValue(instance, Enum.Parse(propertyInfo.PropertyType, value.ToString()));
                }
                else if (propertyInfo.PropertyType.IsNullable() && value == null)
                {
                    propertyInfo.SetValue(instance, null);
                }
                else
                {
                    var pType = propertyInfo.PropertyType;
                    if (pType.IsNullable())
                    {
                        pType = Nullable.GetUnderlyingType(pType);
                    }

                    if (pType.IsString())
                    {
                        propertyInfo.SetValue(instance, value?.ToString());
                    }
                    else if (pType.IsDateTime() || pType.IsBool())
                    {
                        propertyInfo.SetValue(instance, value);
                    }
                    else if (pType.IsGuid())
                    {
                        propertyInfo.SetValue(instance, Guid.Parse(value.ToString()));
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, Convert.ChangeType(value, pType));
                    }
                }

                #endregion

                var storageModel = new ModuleOptionStorageModel
                {
                    Key = definition.Key,
                    Value = value?.ToString(),
                    Remarks = definition.Name
                };

                storageModels.Add(storageModel);
            }

            //持久化
            _storageProvider.Save(storageModels);

            foreach (var changedEventType in descriptor.ChangedEvents)
            {
                var changedEvent = _serviceProvider.GetService(changedEventType);
                var method = changedEventType.GetMethod("OnChanged");
                if (method != null)
                {
                    method.Invoke(changedEvent, new object[] { instance, oldInstance });
                }
            }
        }
    }
}
