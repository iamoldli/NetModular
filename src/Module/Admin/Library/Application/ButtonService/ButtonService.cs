using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.ButtonService.ViewModels;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.RoleMenuButton;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.ButtonService
{
    public class ButtonService : IButtonService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IButtonRepository _buttonRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleMenuButtonRepository _roleMenuButtonRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IButtonPermissionRepository _buttonPermissionRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IAccountService _accountService;

        public ButtonService(IMapper mapper, IUnitOfWork<AdminDbContext> uow, IButtonRepository buttonRepository, IMenuRepository menuRepository, IRoleMenuButtonRepository roleMenuButtonRepository, IPermissionRepository permissionRepository, IButtonPermissionRepository buttonPermissionRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService)
        {
            _mapper = mapper;
            _uow = uow;
            _buttonRepository = buttonRepository;
            _menuRepository = menuRepository;
            _roleMenuButtonRepository = roleMenuButtonRepository;
            _permissionRepository = permissionRepository;
            _buttonPermissionRepository = buttonPermissionRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountService = accountService;
        }

        public async Task<IResultModel> Query(ButtonQueryModel model)
        {
            var result = new QueryResultModel<Button>();
            var paging = model.Paging();
            result.Rows = await _buttonRepository.Query(paging, model.MenuId, model.Name);
            result.Total = paging.TotalCount;

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Sync(ButtonSyncModel model)
        {
            if (model.Buttons == null || !model.Buttons.Any())
                return ResultModel.Failed("请选择按钮");

            var menu = await _menuRepository.GetAsync(model.MenuId);
            if (menu == null)
                return ResultModel.Failed("菜单不存在");

            foreach (var info in model.Buttons)
            {
                if (info.Name.IsNull())
                {
                    return ResultModel.Failed("按钮名称不能为空");
                }

                if (info.Code.IsNull())
                {
                    return ResultModel.Failed($"按钮({info.Name})编码不能为空");
                }
            }

            _uow.BeginTransaction();

            foreach (var button in model.Buttons)
            {
                button.MenuId = model.MenuId;
                button.Code = button.Code.ToLower();

                if (!await _buttonRepository.Exists(button.Code))
                {
                    if (!await _buttonRepository.AddAsync(button))
                    {
                        _uow.Rollback();
                        return ResultModel.Failed("同步失败");
                    }
                }
                else
                {
                    if (!await _buttonRepository.UpdateForSync(button))
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
            var entity = await _buttonRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (await _roleMenuButtonRepository.ExistsWidthButton(id))
                return ResultModel.Failed("有角色绑定了该按钮，请先删除绑定关系");

            _uow.BeginTransaction();

            if (await _roleMenuButtonRepository.DeleteByButton(entity.Id) && await _buttonRepository.DeleteAsync(id))
            {
                _uow.Commit();
                return ResultModel.Success();
            }
            _uow.Rollback();
            return ResultModel.Failed();
        }

        public async Task<IResultModel> QueryPermissionList(Guid id)
        {
            var list = await _permissionRepository.QueryByButton(id);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindPermission(ButtonBindPermissionModel model)
        {
            _uow.BeginTransaction();

            //删除旧数据
            if (await _buttonPermissionRepository.RemoveByButtonId(model.Id))
            {
                if (model.PermissionList == null || !model.PermissionList.Any())
                {
                    _uow.Commit();
                    await ClearAccountPermissionCache(model.Id);
                    return ResultModel.Success();
                }

                //添加新数据
                var entityList = new List<ButtonPermission>();
                model.PermissionList.ForEach(p =>
                {
                    entityList.Add(new ButtonPermission { ButtonId = model.Id, PermissionId = p });
                });
                if (await _buttonPermissionRepository.AddAsync(entityList))
                {
                    _uow.Commit();
                    await ClearAccountPermissionCache(model.Id);
                    return ResultModel.Success();
                }
            }

            _uow.Rollback();
            return ResultModel.Failed();
        }

        /// <summary>
        /// 清除角色关联账户的权限缓存
        /// </summary>
        /// <returns></returns>
        private async Task ClearAccountPermissionCache(Guid buttonId)
        {
            var relationList = await _accountRoleRepository.QueryByButton(buttonId);
            if (relationList.Any())
            {
                foreach (var relation in relationList)
                {
                    _accountService.ClearPermissionListCache(relation.AccountId);
                }
            }
        }
    }
}
