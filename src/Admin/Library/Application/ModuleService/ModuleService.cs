using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Options.Abstraction;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleService.ViewModels;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.Module;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.ModuleService
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repository;
        private readonly IAuditInfoRepository _auditInfoRepository;
        private readonly IModuleCollection _moduleCollection;
        private readonly AdminDbContext _dbContext;
        private readonly IModuleOptionsEngine _moduleOptionsContainer;
        private readonly ILogger _logger;
        private readonly AdminOptions _options;

        public ModuleService(IModuleRepository repository, IModuleCollection moduleCollection, AdminDbContext dbContext, ILogger<ModuleService> logger, IModuleOptionsEngine moduleOptionsContainer, IAuditInfoRepository auditInfoRepository, AdminOptions options)
        {
            _repository = repository;
            _moduleCollection = moduleCollection;
            _dbContext = dbContext;
            _logger = logger;
            _moduleOptionsContainer = moduleOptionsContainer;
            _auditInfoRepository = auditInfoRepository;
            _options = options;
        }

        public async Task<IResultModel> Query()
        {
            var list = await _repository.GetAllAsync();
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> Sync()
        {
            if (!_options.RefreshModuleOrPermission)
                return ResultModel.Success();

            var modules = _moduleCollection.Select(m => new ModuleEntity
            {
                Name = m.Name,
                Code = m.Id,
                Icon = m.Icon,
                Version = m.Version,
                Remarks = m.Description
            });

            _logger.LogDebug("Sync Module Info");

            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var module in modules)
                {
                    if (!await _repository.Exists(module, uow))
                    {
                        if (!await _repository.AddAsync(module))
                        {
                            uow.Rollback();
                            return ResultModel.Failed();
                        }
                    }
                    else
                    {
                        if (!await _repository.UpdateByCode(module))
                        {
                            uow.Rollback();
                            return ResultModel.Failed();
                        }
                    }
                }

                uow.Commit();
            }

            return ResultModel.Success();
        }

        public IResultModel Select()
        {
            var list = _moduleCollection.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id,
                Data = new
                {
                    m.Id,
                    m.Name,
                    m.Icon,
                    m.Description
                }
            }).ToList();

            return ResultModel.Success(list);
        }

        public IResultModel OptionsEdit(string code)
        {
            var model = new ModuleOptionsUpdateModel
            {
                Code = code,
                Definitions = _moduleOptionsContainer.GetDefinitions(code),
                Instance = _moduleOptionsContainer.GetInstance(code)
            };

            return ResultModel.Success(model);
        }

        public IResultModel OptionsUpdate(ModuleOptionsUpdateModel model)
        {
            if (model.Values != null && model.Values.Any())
            {
                _moduleOptionsContainer.RefreshInstance(model.Code, model.Values);
                return ResultModel.Success("保存成功");
            }

            return ResultModel.Failed("配置信息不存在");
        }

        public async Task<bool> SyncApiRequestCount()
        {
            var counts = await _auditInfoRepository.QueryCountByModule();
            if (!counts.Any())
                return true;

            using var uow = _dbContext.NewUnitOfWork();

            var tasks = new List<Task>();
            foreach (var option in counts)
            {
                tasks.Add(_repository.UpdateApiRequestCount(option.Label, option.Value.ToLong(), uow));
            }

            await Task.WhenAll(tasks);

            uow.Commit();

            return true;
        }
    }
}
