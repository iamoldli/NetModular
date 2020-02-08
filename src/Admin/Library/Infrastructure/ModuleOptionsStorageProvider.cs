using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Options.Abstraction;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Infrastructure
{
    /// <summary>
    /// 系统配置存储实现
    /// </summary>
    public class ModuleOptionsStorageProvider : IModuleOptionsStorageProvider
    {
        private readonly IConfigRepository _repository;
        private readonly AdminDbContext _dbContext;

        public ModuleOptionsStorageProvider(IConfigRepository repository, AdminDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public async Task<IList<ModuleOptionDescriptor>> GetAll()
        {
            var list = await _repository.QueryByType(ConfigType.Module);
            return list.Select(m => new ModuleOptionDescriptor
            {
                Key = m.Key,
                Value = m.Value,
                Remarks = m.Remarks
            }).ToList();
        }

        public async Task Save(List<ModuleOptionDescriptor> descriptors)
        {
            if (descriptors == null || !descriptors.Any())
                return;

            using var uow = _dbContext.NewUnitOfWork();
            foreach (var descriptor in descriptors)
            {
                var newEntity = new ConfigEntity
                {
                    Type = ConfigType.Module,
                    Key = descriptor.Key,
                    Value = descriptor.Value,
                    Remarks = descriptor.Remarks
                };

                var entity = await _repository.GetByKey(descriptor.Key, ConfigType.Module);
                if (entity == null)
                {
                    await _repository.AddAsync(newEntity, uow);
                }
                else
                {
                    newEntity.Id = entity.Id;
                    await _repository.UpdateAsync(newEntity, uow);
                }
            }
        }
    }
}
