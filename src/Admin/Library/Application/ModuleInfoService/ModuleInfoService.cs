using System;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Domain.ModuleInfo.Models;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Infrastructure.Repositories;
using ModuleInfoEntity = NetModular.Module.Admin.Domain.ModuleInfo.ModuleInfoEntity;

namespace NetModular.Module.Admin.Application.ModuleInfoService
{
    public class ModuleInfoService : IModuleInfoService
    {
        private readonly IModuleInfoRepository _repository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IModuleCollection _moduleCollection;
        private readonly AdminDbContext _dbContext;

        public ModuleInfoService(IModuleInfoRepository repository, IPermissionRepository permissionRepository, IMenuRepository menuRepository, IModuleCollection moduleCollection, AdminDbContext dbContext)
        {
            _repository = repository;
            _permissionRepository = permissionRepository;
            _menuRepository = menuRepository;
            _moduleCollection = moduleCollection;
            _dbContext = dbContext;
        }

        public async Task<IResultModel> Query(ModuleInfoQueryModel model)
        {
            var result = new QueryResultModel<ModuleInfoEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Sync()
        {
            var modules = _moduleCollection.Select(m => new ModuleInfoEntity
            {
                Name = m.Name,
                Code = m.Id,
                Icon = m.Icon,
                Version = m.Version
            });

            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var moduleInfo in modules)
                {
                    if (!await _repository.Exists(moduleInfo, uow))
                    {
                        if (!await _repository.AddAsync(moduleInfo))
                        {
                            uow.Rollback();
                            return ResultModel.Failed();
                        }
                    }
                    else
                    {
                        if (!await _repository.UpdateByCode(moduleInfo))
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

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var exists = await _permissionRepository.ExistsWidthModule(entity.Code);
            if (exists)
                return ResultModel.Failed("有权限绑定了该模块，请先删除绑定关系");

            exists = await _menuRepository.ExistsWidthModule(entity.Code);
            if (exists)
                return ResultModel.Failed("有菜单绑定了该模块，请先删除绑定关系");

            var result = await _repository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var all = await _repository.GetAllAsync();
            var list = all.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code,
                Data = m.Icon
            }).ToList();
            return ResultModel.Success(list);
        }
    }
}
