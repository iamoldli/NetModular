using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Config.Abstraction;
using NetModular.Module.Admin.Domain.Config;

namespace NetModular.Module.Admin.Infrastructure
{
    public class ConfigStorage : IConfigStorage
    {
        private readonly IConfigRepository _repository;

        public ConfigStorage(IConfigRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<ConfigDescriptor>> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return list.Select(m => new ConfigDescriptor
            {
                Key = m.Key,
                Value = m.Value,
                Remarks = m.Remarks
            }).ToList();
        }

        public async Task Add(ConfigDescriptor descriptor)
        {
            if (descriptor == null)
                return;

            await _repository.AddAsync(new ConfigEntity
            {
                Key = descriptor.Key,
                Value = descriptor.Value,
                Remarks = descriptor.Remarks
            });
        }
    }
}
