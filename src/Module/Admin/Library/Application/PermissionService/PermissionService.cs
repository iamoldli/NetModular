using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.PermissionService.ViewModels;
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.MenuPermission;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMenuPermissionRepository _menuPermissionRepository;
        private readonly IButtonPermissionRepository _buttonPermissionRepository;
        private readonly IUnitOfWork _uow;

        public PermissionService(IPermissionRepository permissionRepository, IMenuPermissionRepository menuPermissionRepository, IButtonPermissionRepository buttonPermissionRepository, IUnitOfWork<AdminDbContext> uow)
        {
            _permissionRepository = permissionRepository;
            _menuPermissionRepository = menuPermissionRepository;
            _buttonPermissionRepository = buttonPermissionRepository;
            _uow = uow;
        }

        public async Task<IResultModel> Query(PermissionQueryModel model)
        {
            var queryResult = new QueryResultModel<Permission>();
            var paging = model.Paging();
            queryResult.Rows = await _permissionRepository.Query(paging, model.ModuleCode, model.Name, model.Controller, model.Action);
            queryResult.Total = paging.TotalCount;

            return ResultModel.Success(queryResult);
        }

        public async Task<IResultModel> Sync(List<Permission> permissions)
        {
            if (permissions == null || !permissions.Any())
                return ResultModel.Failed("未找到权限信息");

            _uow.BeginTransaction();

            foreach (var permission in permissions)
            {
                if (!await _permissionRepository.Exists(permission))
                {
                    if (!await _permissionRepository.AddAsync(permission))
                    {
                        _uow.Rollback();
                        return ResultModel.Failed("同步失败");
                    }
                }
                else
                {
                    if (!await _permissionRepository.UpdateForSync(permission))
                    {
                        _uow.Rollback();
                        return ResultModel.Failed("同步失败");
                    }
                }
            }

            _uow.Commit();

            return ResultModel.Success();
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
    }
}
