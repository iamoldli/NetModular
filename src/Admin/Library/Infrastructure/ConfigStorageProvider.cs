using System;
using System.Threading.Tasks;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Infrastructure
{
    [Singleton]
    public class ConfigStorageProvider : IConfigStorageProvider
    {
        private readonly IConfigRepository _repository;

        public ConfigStorageProvider(IConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetJson(ConfigType type, string code)
        {
            if (code.IsNull())
                return string.Empty;

            var entity = await _repository.Get(type, code);
            return entity == null ? string.Empty : entity.Value;
        }

        public async Task<bool> SaveJson(ConfigType type, string code, string json)
        {
            if (code.IsNull())
                throw new NullReferenceException("编码不能为空");

            var entity = await _repository.Get(type, code) ?? new ConfigEntity();

            entity.Type = type;
            entity.Code = code;
            entity.Value = json;

            if (entity.Id > 0)
                return await _repository.UpdateAsync(entity);

            return await _repository.AddAsync(entity);
        }
    }
}
