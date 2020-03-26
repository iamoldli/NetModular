using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Infrastructure
{
    public class SystemConfigResolver
    {
        private readonly IConfigRepository _configRepository;
        private readonly ILogger _logger;

        public SystemConfigResolver(IConfigRepository configRepository, ILogger<SystemConfigResolver> logger)
        {
            _configRepository = configRepository;
            _logger = logger;
        }

        /// <summary>
        /// 加载系统配置
        /// </summary>
        /// <returns></returns>
        public async Task<SystemConfigModel> Load()
        {
            var configList = await _configRepository.QueryByType(ConfigType.System);
            var model = new SystemConfigModel();

            GetConfig(model, configList);

            return model;
        }

        private void GetConfig(object config, IList<ConfigEntity> list)
        {
            var properties = config.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (Type.GetTypeCode(p.PropertyType) != TypeCode.Object)
                {
                    var descAttr = (ConfigDescriptionAttribute)Attribute.GetCustomAttribute(p, typeof(ConfigDescriptionAttribute));
                    if (descAttr != null)
                    {
                        var entity = list.FirstOrDefault(m => m.Key.EqualsIgnoreCase(descAttr.Name));
                        if (entity != null)
                        {
                            try
                            {
                                p.SetValue(config, Convert.ChangeType(entity.Value, p.PropertyType));
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("加载系统配置错误：{@ex}", ex);
                            }
                        }
                    }
                }
                else
                {
                    GetConfig(p.GetValue(config), list);
                }
            }
        }
    }
}
