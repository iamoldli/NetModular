using System.Threading.Tasks;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Domain.Config.Models;

namespace NetModular.Module.Admin.Application.ConfigService
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepository _repository;
        private readonly IModuleOptionsEngine _moduleOptionsEngine;

        public ConfigService(IConfigRepository repository, IModuleOptionsEngine moduleOptionsEngine)
        {
            _repository = repository;
            _moduleOptionsEngine = moduleOptionsEngine;
        }

        public async Task<IResultModel> Query(ConfigQueryModel model)
        {
            model.Type = ConfigType.Custom;
            var result = new QueryResultModel<ConfigEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ConfigAddModel model)
        {
            if (await _repository.Exists(ConfigType.Custom, model.Key))
                return ResultModel.HasExists;

            var entity = new ConfigEntity
            {
                Type = ConfigType.Custom,
                Key = model.Key,
                Value = model.Value,
                Remarks = model.Remarks
            };

            var result = await _repository.AddAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("配置不存在");
            if (entity.Type != ConfigType.Custom)
                return ResultModel.Failed("非自定义配置不允许删除");

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(int id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("配置不存在");
            if (entity.Type != ConfigType.Custom)
                return ResultModel.Failed("非自定义配置不允许编辑");

            var model = new ConfigUpdateModel
            {
                Id = id,
                Key = entity.Key,
                Value = entity.Value,
                Remarks = entity.Remarks
            };

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ConfigUpdateModel model)
        {
            var entity = await _repository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.Failed("配置不存在");
            if (entity.Type != ConfigType.Custom)
                return ResultModel.Failed("非自定义配置不允许编辑");

            entity.Key = model.Key;
            entity.Value = model.Value;
            entity.Remarks = model.Remarks;

            if (await _repository.Exists(entity))
                return ResultModel.Failed($"{model.Key}键已存在");

            var result = await _repository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> GetValueByKey(string key, ConfigType type = ConfigType.System, string moduleCode = null)
        {
            string value = string.Empty;
            if (type == ConfigType.Module)
            {
                var options = _moduleOptionsEngine.GetInstance(moduleCode);
                if (options != null)
                {
                    var properties = options.GetType().GetProperties();
                    foreach (var propertyInfo in properties)
                    {
                        if (propertyInfo.Name.EqualsIgnoreCase(key))
                        {
                            var val = propertyInfo.GetValue(options);
                            if (val != null)
                            {
                                if (propertyInfo.PropertyType.IsEnum || propertyInfo.PropertyType.IsBool())
                                {
                                    value = val.ToInt().ToString();
                                }
                                else if (propertyInfo.PropertyType.IsDateTime())
                                {
                                    value = val.ToDateTime().Format();
                                }
                                else
                                {
                                    value = val.ToString();
                                }
                            }

                            break;
                        }
                    }
                }
            }
            else
            {
                var entity = await _repository.GetByKey(key);
                if (entity != null)
                {
                    value = entity.Value;
                }
            }

            return ResultModel.Success(value);
        }
    }
}
