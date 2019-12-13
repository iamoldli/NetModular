using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Application.SystemService
{
    [Singleton]
    public class SystemConfigResolver
    {
        private readonly IConfigRepository _configRepository;

        public SystemConfigResolver(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
        }

        public async Task<SystemConfigModel> GetConfig()
        {
            var configList = await _configRepository.QueryByPrefix("sys_");
            var model = new SystemConfigModel();

            GetConfig(model, configList);

            if (model.Base.Logo.NotNull())
            {
                model.Base.Logo = new Uri($"/upload/{model.Base.Logo}").ToString().ToLower();
            }

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
                            catch
                            {
                                //
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
