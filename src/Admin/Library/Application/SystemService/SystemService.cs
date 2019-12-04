using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.SystemService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.SystemService
{
    public class SystemService : ISystemService
    {
        private readonly IConfigRepository _configRepository;
        private readonly ICacheHandler _cache;
        private readonly AdminDbContext _dbContext;

        public SystemService(IConfigRepository configRepository, ICacheHandler cache, AdminDbContext dbContext)
        {
            _configRepository = configRepository;
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task<IResultModel<SystemConfigModel>> GetConfig(string host = null)
        {
            var result = new ResultModel<SystemConfigModel>();
            if (!_cache.TryGetValue(CacheKeys.SystemConfigCacheKey, out SystemConfigModel model))
            {
                var configList = await _configRepository.QueryByPrefix("sys_");
                model = new SystemConfigModel();

                GetConfig(model, configList);

                await _cache.SetAsync(CacheKeys.SystemConfigCacheKey, model);
            }

            if (model.Base.Logo.NotNull() && !model.Base.Logo.StartsWith("http") && host.NotNull())
            {
                model.Base.Logo = new Uri($"{host}/upload/{model.Base.Logo}").ToString().ToLower();
            }

            return result.Success(model);
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

        public IResultModel UpdateBaseConfig(SystemBaseConfigModel model)
        {
            Update(model);
            return ResultModel.Success();
        }

        public IResultModel UpdateComponentConfig(SystemComponentConfigModel model)
        {
            Update(model);
            return ResultModel.Success();
        }

        public IResultModel UpdateLoginConfig(SystemLoginConfigModel model)
        {
            Update(model);
            return ResultModel.Success();
        }

        public IResultModel UpdatePermissionConfig(SystemPermissionConfigModel model)
        {
            Update(model);
            return ResultModel.Success();
        }

        public IResultModel UpdateToolbarConfig(SystemToolbarConfigModel model)
        {
            Update(model);
            return ResultModel.Success();
        }

        private void UpdateConfig<T>(T config, List<ConfigEntity> list)
        {
            var properties = config.GetType().GetProperties();
            foreach (var p in properties)
            {
                var val = p.GetValue(config);
                if (Type.GetTypeCode(p.PropertyType) != TypeCode.Object)
                {
                    var descAttr = (ConfigDescriptionAttribute)Attribute.GetCustomAttribute(p, typeof(ConfigDescriptionAttribute));
                    if (descAttr != null)
                    {
                        list.Add(new ConfigEntity
                        {
                            Key = descAttr.Name,
                            Value = val != null ? val.ToString() : "",
                            Remarks = descAttr.Remarks
                        });
                    }
                }
                else
                {
                    UpdateConfig(val, list);
                }
            }
        }

        private void Update<T>(T config)
        {
            var list = new List<ConfigEntity>();

            UpdateConfig(config, list);

            using var uow = _dbContext.NewUnitOfWork();
            var tasks = new Task[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tasks[i] = _configRepository.UpdateAsync(list[i], uow);
            }

            Task.WaitAll(tasks);
            uow.Commit();

            _cache.RemoveAsync(CacheKeys.SystemConfigCacheKey).GetAwaiter().GetResult();
        }
    }
}
