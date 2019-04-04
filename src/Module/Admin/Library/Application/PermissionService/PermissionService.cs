using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.PermissionService.ViewModels;
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.MenuPermission;
using NetModular.Module.Admin.Domain.Permission;

namespace NetModular.Module.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly LoginInfo _loginInfo;
        private readonly IMapper _mapper;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuPermissionRepository _menuPermissionRepository;
        private readonly IButtonPermissionRepository _buttonPermissionRepository;

        public PermissionService(LoginInfo loginInfo, IMapper mapper, IPermissionRepository permissionRepository, IMenuPermissionRepository menuPermissionRepository, IButtonPermissionRepository buttonPermissionRepository)
        {
            _loginInfo = loginInfo;
            _mapper = mapper;
            _permissionRepository = permissionRepository;
            _menuPermissionRepository = menuPermissionRepository;
            _buttonPermissionRepository = buttonPermissionRepository;
        }

        public async Task<IResultModel> Query(PermissionQueryModel model)
        {
            var queryResult = new QueryResultModel<Permission>();
            var paging = model.Paging();
            queryResult.Rows = await _permissionRepository.Query(paging, model.ModuleCode, model.Name, model.Controller, model.Action);
            queryResult.Total = paging.TotalCount;

            return ResultModel.Success(queryResult);
        }

        public async Task<IResultModel> Add(PermissionAddModel model)
        {
            var list = new List<Permission>();

            foreach (var action in model.Actions)
            {
                var entity = new Permission
                {
                    Name = $"{model.ControllerName}-{action.Value}",
                    ModuleCode = model.ModuleCode.ToLower(),
                    Controller = model.Controller.ToLower(),
                    Action = action.Key.ToLower(),
                    CreatedBy = _loginInfo.AccountId
                };

                if (await _permissionRepository.Exists(entity))
                    return ResultModel.Failed($"{action}操作已存在~");

                list.Add(entity);
            }

            var result = await _permissionRepository.AddAsync(list);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _permissionRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("模块不存在");

            var model = _mapper.Map<PermissionUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(PermissionUpdateModel model)
        {
            var entity = _mapper.Map<Permission>(model);
            if (await _permissionRepository.Exists(entity))
                return ResultModel.HasExists;

            entity = await _permissionRepository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.NotExists;

            entity = _mapper.Map(model, entity);

            var result = await _permissionRepository.UpdateAsync(entity);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            if (!await _permissionRepository.Exists(id))
                return ResultModel.Failed("该记录不存在~");

            if (await _menuPermissionRepository.ExistsBindPermission(id))
                return ResultModel.Failed("有菜单关联了该权限，请先删除关联的菜单~");

            if (await _buttonPermissionRepository.ExistsBindPermission(id))
                return ResultModel.Failed("有按钮关联了该权限，请先删除关联的按钮~");

            var result = await _permissionRepository.DeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<List<Permission>> QueryControllerActions(string moduleCode, string controller)
        {
            var model = new PermissionQueryModel
            {
                ModuleCode = moduleCode,
                Controller = controller,
                Page = new QueryPagingModel
                {
                    Index = 1,
                    Size = 1000
                }
            };
            var paging = model.Paging();
            return (await _permissionRepository.Query(paging, model.ModuleCode, model.Name, model.Controller, model.Action)).ToList();
        }
    }
}
