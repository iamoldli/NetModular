using System.Collections.Generic;
using System.Linq;
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

        public IList<ModuleOptionStorageModel> GetAll()
        {
            var list = _repository.QueryByType(ConfigType.Module).Result;
            return list.Select(m => new ModuleOptionStorageModel
            {
                Key = m.Key,
                Value = m.Value,
                Remarks = m.Remarks
            }).ToList();
        }

        public void Save(List<ModuleOptionStorageModel> descriptors)
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

                var entity = _repository.GetByKey(descriptor.Key, ConfigType.Module).Result;
                if (entity == null)
                {
                    _repository.AddAsync(newEntity, uow).GetAwaiter().GetResult();
                }
                else
                {
                    newEntity.Id = entity.Id;
                    _repository.UpdateAsync(newEntity, uow).GetAwaiter().GetResult();
                }
            }
        }
    }
}
