using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NetModular.Lib.Config.Abstractions;
using Newtonsoft.Json;

namespace NetModular.Lib.Config.Core
{
    public class ConfigProvider : IConfigProvider
    {
        /// <summary>
        /// 含有了实例的配置描述符集合
        /// </summary>
        private static readonly ConfigCollection ConfigWidthInstanceCollection = new ConfigCollection();

        private readonly IConfigCollection _configs;
        private readonly IConfigStorageProvider _storageProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _cfg;

        public ConfigProvider(IConfigCollection configs, IConfigStorageProvider storageProvider, IServiceProvider serviceProvider, IConfiguration cfg)
        {
            _configs = configs;
            _storageProvider = storageProvider;
            _serviceProvider = serviceProvider;
            _cfg = cfg;
        }

        public IConfig Get(Type implementType)
        {
            var descriptor = GetDescriptor(m => m.ImplementType == implementType);
            if (descriptor.Instance == null)
            {
                var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
                if (json.IsNull())
                {
                    descriptor.Instance = (IConfig)Activator.CreateInstance(implementType);
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
                else
                    descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, implementType);
            }

            return descriptor.Instance;
        }

        public IConfig Get(string code, ConfigType type)
        {
            var descriptor = GetDescriptor(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
            if (descriptor.Instance == null)
            {
                var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
                if (json.IsNull())
                {
                    descriptor.Instance = (IConfig)Activator.CreateInstance(descriptor.ImplementType);
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
                else
                    descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);
            }

            return descriptor.Instance;
        }

        public TConfig Get<TConfig>() where TConfig : IConfig, new()
        {
            var descriptor = GetDescriptor(m => m.ImplementType == typeof(TConfig));
            if (descriptor.Instance == null)
            {
                var json = _storageProvider.GetJson(descriptor.Type, descriptor.Code).Result;
                if (json.IsNull())
                {
                    descriptor.Instance = new TConfig();
                    var section = _cfg.GetSection(descriptor.Code);
                    if (section != null)
                    {
                        section.Bind(descriptor.Instance);
                    }
                }
                else
                    descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);
            }

            return (TConfig)descriptor.Instance;
        }

        public bool Set(ConfigType type, string code, string json)
        {
            if (json.IsNull())
                return false;

            var descriptor = GetDescriptor(m => m.Code.EqualsIgnoreCase(code) && m.Type == type);
            //旧实例
            var oldInstance = descriptor.Instance;
            //新实例
            descriptor.Instance = (IConfig)JsonConvert.DeserializeObject(json, descriptor.ImplementType);

            //持久化
            _storageProvider.SaveJson(type, descriptor.Code, json);

            #region ==触发变更事件==

            foreach (var changeEventType in descriptor.ChangeEvents)
            {
                var eventInstance = _serviceProvider.GetService(changeEventType);
                var method = changeEventType.GetMethod("OnChanged");
                if (method != null)
                {
                    method.Invoke(eventInstance, new object[] { descriptor.Instance, oldInstance });
                }
            }

            #endregion

            return true;
        }

        private ConfigDescriptor GetDescriptor(Func<ConfigDescriptor, bool> predicate)
        {
            var descriptor = ConfigWidthInstanceCollection.FirstOrDefault(predicate);
            if (descriptor == null)
            {
                descriptor = _configs.Where(predicate).Select(m => new ConfigDescriptor
                {
                    Type = m.Type,
                    Code = m.Code,
                    ImplementType = m.ImplementType,
                    ChangeEvents = m.ChangeEvents
                }).FirstOrDefault();

                ConfigWidthInstanceCollection.Add(descriptor);
            }

            if (descriptor == null)
                throw new NullReferenceException("指定类型的配置实例不存在");

            return descriptor;
        }
    }
}
