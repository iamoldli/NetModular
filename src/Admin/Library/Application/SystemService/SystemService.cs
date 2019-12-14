using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.SystemService
{
    public class SystemService : ISystemService
    {
        private readonly IConfigRepository _configRepository;
        private readonly AdminDbContext _dbContext;
        private readonly SystemConfigModel _configModel;

        public SystemService(IConfigRepository configRepository, AdminDbContext dbContext, SystemConfigModel configModel)
        {
            _configRepository = configRepository;
            _dbContext = dbContext;
            _configModel = configModel;
        }

        public IResultModel UpdateBaseConfig(SystemBaseConfigModel model)
        {
            Update(model);
            _configModel.Base = model;
            return ResultModel.Success();
        }

        public IResultModel UpdateComponentConfig(SystemComponentConfigModel model)
        {
            Update(model);
            _configModel.Component = model;
            return ResultModel.Success();
        }

        public IResultModel UpdateLoginConfig(SystemLoginConfigModel model)
        {
            Update(model);
            _configModel.Login = model;
            return ResultModel.Success();
        }

        public IResultModel UpdatePermissionConfig(SystemPermissionConfigModel model)
        {
            Update(model);
            _configModel.Permission = model;
            return ResultModel.Success();
        }

        public IResultModel UpdateToolbarConfig(SystemToolbarConfigModel model)
        {
            Update(model);
            _configModel.Toolbar = model;
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
        }
    }
}
