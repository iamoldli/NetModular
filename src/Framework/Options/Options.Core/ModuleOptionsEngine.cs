using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Extensions;

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
                            SetValue(options, definition.PropertyInfo, val.Value);
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

            //通过Copy方法创建旧实例
            var oldInstance = instance.Copy();

            var storageModels = new List<ModuleOptionStorageModel>();
            foreach (var definition in descriptor.Definitions)
            {
                var value = values.FirstOrDefault(m => m.Key.EqualsIgnoreCase(definition.DataName)).Value;

                SetValue(instance, definition.PropertyInfo, value);

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

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="options"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        private void SetValue(IModuleOptions options, PropertyInfo property, object value)
        {
            var propertyType = property.PropertyType;
            if (propertyType.IsNullable() && value == null)
            {
                property.SetValue(options, null);
                return;
            }
            if (propertyType.IsNullable())
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
            }

            if (value == null)
            {
                property.SetValue(options, default);
                return;
            }

            if (propertyType.IsEnum)
            {
                property.SetValue(options, Enum.Parse(propertyType, value.ToString()));
            }
            else if (propertyType.IsString())
            {
                property.SetValue(options, value);
            }
            else if (propertyType.IsByte())
            {
                property.SetValue(options, value.ToByte());
            }
            else if (propertyType.IsChar())
            {
                property.SetValue(options, value.ToChar());
            }
            else if (propertyType.IsShort())
            {
                property.SetValue(options, value.ToShort());
            }
            else if (propertyType.IsInt())
            {
                property.SetValue(options, value.ToInt());
            }
            else if (propertyType.IsLong())
            {
                property.SetValue(options, value.ToLong());
            }
            else if (propertyType.IsFloat())
            {
                property.SetValue(options, value.ToFloat());
            }
            else if (propertyType.IsDouble())
            {
                property.SetValue(options, value.ToDouble());
            }
            else if (propertyType.IsDecimal())
            {
                property.SetValue(options, value.ToDecimal());
            }
            else if (propertyType.IsDateTime())
            {
                property.SetValue(options, value.ToDateTime());
            }
            else if (propertyType.IsBool())
            {
                property.SetValue(options, value.ToBool());
            }
            else if (propertyType.IsGuid())
            {
                property.SetValue(options, value.ToGuid());
            }
        }
    }
}
