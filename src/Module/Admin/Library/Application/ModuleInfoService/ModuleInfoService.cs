using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.ModuleInfoService.ViewModels;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.ModuleInfo;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.ModuleInfoService
{
    public class ModuleInfoService : IModuleInfoService
    {
        private readonly IMapper _mapper;
        private readonly IModuleInfoRepository _repository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuRepository _menuRepository;

        public ModuleInfoService(IMapper mapper, IModuleInfoRepository repository, IPermissionRepository permissionRepository, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _permissionRepository = permissionRepository;
            _menuRepository = menuRepository;
        }

        public async Task<IResultModel> Query(ModuleInfoQueryModel model)
        {
            var result = new QueryResultModel<ModuleInfo>();
            var paging = model.Paging();
            result.Rows = await _repository.Query(paging, model.Name, model.Code);
            result.Total = paging.TotalCount;
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(ModuleInfoAddModel model)
        {
            if (await _repository.Exists(model.Code.ToLower()))
                return ResultModel.HasExists;

            var moduleInfo = _mapper.Map<ModuleInfo>(model);

            var result = await _repository.AddAsync(moduleInfo);

            return ResultModel.Result(result);
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

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            var model = _mapper.Map<ModuleInfoUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(ModuleInfoUpdateModel model)
        {
            if (await _repository.Exists(model.Code.ToLower(), model.Id))
                return ResultModel.HasExists;

            var moduleInfo = await _repository.GetAsync(model.Id);
            _mapper.Map(model, moduleInfo);

            var result = await _repository.UpdateAsync(moduleInfo);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Select()
        {
            var all = await _repository.GetAllAsync();
            var list = all.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Code
            }).ToList();
            return ResultModel.Success(list);
        }
    }
}
