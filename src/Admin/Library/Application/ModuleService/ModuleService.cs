using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Module.Abstractions;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Domain.Module;
using NetModular.Module.Admin.Domain.Permission;
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
        private readonly ILogger _logger;
        private readonly ICacheHandler _cacheHandler;
        private readonly IPermissionRepository _permissionRepository;
        public ModuleService(IModuleRepository repository, IModuleCollection moduleCollection, AdminDbContext dbContext, ILogger<ModuleService> logger, IAuditInfoRepository auditInfoRepository, ICacheHandler cacheHandler, IPermissionRepository permissionRepository)
        {
            _repository = repository;
            _moduleCollection = moduleCollection;
            _dbContext = dbContext;
            _logger = logger;
            _auditInfoRepository = auditInfoRepository;
            _cacheHandler = cacheHandler;
            _permissionRepository = permissionRepository;
        }

        public async Task<IResultModel> Query()
        {
            var list = await _repository.GetAllAsync();
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> Sync(List<PermissionEntity> permissions)
        {
            using var uow = _dbContext.NewUnitOfWork();

            #region ==同步模块信息==

            var modules = _moduleCollection.Select(m => new ModuleEntity
            {
                Number = m.Id,
                Name = m.Name,
                Code = m.Code,
                Icon = m.Icon,
                Version = m.Version,
                Remarks = m.Description
            });

            _logger.LogDebug("Sync Module Info");

            foreach (var module in modules)
            {
                if (!await _repository.Exists(module, uow))
                {
                    if (!await _repository.AddAsync(module))
                    {
                        return ResultModel.Failed();
                    }
                }
                else
                {
                    if (!await _repository.UpdateByCode(module))
                    {
                        return ResultModel.Failed();
                    }
                }
            }

            #endregion

            #region ==同步权限信息=

            if (permissions != null && permissions.Any())
            {
                _logger.LogDebug("Sync Permission Info");

                //先清除已有权限信息
                if (await _permissionRepository.ClearAsync(uow))
                {
                    foreach (var permission in permissions)
                    {
                        if (!await _permissionRepository.AddAsync(permission, uow))
                            return ResultModel.Failed("同步失败");
                    }

                    //删除所有账户的权限缓存
                    await _cacheHandler.RemoveByPrefixAsync(CacheKeys.ACCOUNT_PERMISSIONS);
                }
            }

            #endregion

            uow.Commit();

            return ResultModel.Success();
        }

        public IResultModel Select()
        {
            var list = _moduleCollection.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code,
                Data = new
                {
                    m.Id,
                    m.Code,
                    m.Name,
                    m.Icon,
                    m.Description
                }
            }).ToList();

            return ResultModel.Success(list);
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
