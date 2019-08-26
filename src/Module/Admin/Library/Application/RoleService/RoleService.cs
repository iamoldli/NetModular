using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Application.RoleService.ResultModels;
using Nm.Module.Admin.Application.RoleService.ViewModels;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.Role;
using Nm.Module.Admin.Domain.Role.Models;
using Nm.Module.Admin.Domain.RoleMenu;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Application.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IRoleMenuButtonRepository _roleMenuButtonRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IAccountService _accountService;

        public RoleService(IMapper mapper, IRoleRepository repository, IRoleMenuRepository roleMenuRepository, IRoleMenuButtonRepository roleMenuButtonRepository, IButtonRepository buttonRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _roleMenuRepository = roleMenuRepository;
            _roleMenuButtonRepository = roleMenuButtonRepository;
            _buttonRepository = buttonRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountService = accountService;
            _menuRepository = menuRepository;
        }

        public async Task<IResultModel> Query(RoleQueryModel model)
        {
            var result = new QueryResultModel<RoleEntity>
            {
                Rows = await _repository.Query(model),
                Total = model.TotalCount
            };
            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(RoleAddModel model)
        {
            if (await _repository.Exists(model.Name))
                return ResultModel.HasExists;

            var entity = _mapper.Map<RoleEntity>(model);

            var result = await _repository.AddAsync(entity);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var role = await _repository.GetAsync(id);
            if (role == null)
                return ResultModel.Failed("角色不存在");
            if (role.IsSpecified)
                return ResultModel.Failed("指定角色不允许删除");

            var exist = await _accountRoleRepository.ExistsByRole(id);
            if (exist)
                return ResultModel.Failed("有账户绑定了该角色，请先删除对应绑定关系");

            using (var tran = _repository.BeginTransaction())
            {
                var result = await _repository.SoftDeleteAsync(id, tran);
                if (result)
                {
                    result = await _roleMenuRepository.DeleteByRoleId(id, tran);
                    if (result)
                    {
                        result = await _roleMenuButtonRepository.DeleteByRole(id, tran);
                        if (result)
                        {
                            tran.Commit();
                        }
                    }
                }
                return ResultModel.Result(result);
            }
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;

            if (entity.IsSpecified)
                return ResultModel.Failed("指定角色不允许编辑");

            var model = _mapper.Map<RoleUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(RoleUpdateModel model)
        {
            if (await _repository.Exists(model.Name, model.Id))
                return ResultModel.HasExists;

            var role = await _repository.GetAsync(model.Id);
            if (role.IsSpecified)
                return ResultModel.Failed("指定角色不允许编辑");

            _mapper.Map(model, role);

            var result = await _repository.UpdateAsync(role);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> MenuList(Guid id)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return ResultModel.NotExists;

            var list = await _roleMenuRepository.GetByRoleId(id);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindMenu(RoleMenuBindModel model)
        {
            var exists = await _repository.ExistsAsync(model.Id);
            if (!exists)
                return ResultModel.NotExists;

            List<RoleMenuEntity> entityList = null;
            if (model.Menus != null && model.Menus.Any())
            {
                entityList = model.Menus.Select(m => new RoleMenuEntity { RoleId = model.Id, MenuId = m }).ToList();
            }

            /*
             * 操作逻辑
             * 1、清除已有的绑定数据
             * 2、添加新的绑定数据
             */
            using (var tran = _roleMenuRepository.BeginTransaction())
            {
                var clear = await _roleMenuRepository.DeleteByRoleId(model.Id, tran);
                if (clear)
                {
                    if (entityList == null || !entityList.Any() || await _roleMenuRepository.AddAsync(entityList, tran))
                    {
                        tran.Commit();
                        await ClearAccountPermissionCache(model.Id);
                        return ResultModel.Success();
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> MenuButtonList(Guid id, Guid menuId)
        {
            var exists = await _repository.ExistsAsync(id);
            if (!exists)
                return ResultModel.NotExists;

            var list = new List<RoleMenuButtonModel>();
            var data = await _roleMenuButtonRepository.Query(id, menuId);
            if (data.Any())
            {
                foreach (var button in data)
                {
                    list.Add(new RoleMenuButtonModel
                    {
                        Id = button.Id,
                        Name = button.Name,
                        Checked = button.RoleId != Guid.Empty
                    });
                }
            }

            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindMenuButton(RoleMenuButtonBindModel model)
        {
            var exists = await _repository.ExistsAsync(model.RoleId);
            if (!exists)
                return ResultModel.NotExists;

            var menu = await _menuRepository.GetAsync(model.MenuId);
            if (menu == null)
                return ResultModel.Failed("菜单不存在");

            bool result;
            if (model.ButtonId.NotEmpty())
            {
                #region ==单个按钮==

                var entity = _mapper.Map<RoleMenuButtonEntity>(model);
                //如果已存在
                if (await _roleMenuButtonRepository.Exists(entity))
                {
                    if (model.Checked)
                    {
                        return ResultModel.Success();
                    }

                    result = await _roleMenuButtonRepository.Delete(entity);

                    await ClearAccountPermissionCache(model.RoleId);

                    return ResultModel.Result(result);
                }

                if (!model.Checked)
                    return ResultModel.Success();

                result = await _roleMenuButtonRepository.AddAsync(entity);

                await ClearAccountPermissionCache(model.RoleId);

                return ResultModel.Result(result);

                #endregion
            }

            #region ==批量添加指定菜单的所有按钮==

            using (var tran = _roleMenuRepository.BeginTransaction())
            {
                result = await _roleMenuButtonRepository.Delete(model.RoleId, model.MenuId, tran);
                if (result)
                {
                    if (model.Checked)
                    {
                        var buttons = await _buttonRepository.QueryByMenu(menu.RouteName, tran);
                        var entities = buttons.Select(m => new RoleMenuButtonEntity
                        {
                            RoleId = model.RoleId,
                            MenuId = model.MenuId,
                            ButtonId = m.Id
                        }).ToList();

                        if (await _roleMenuButtonRepository.AddAsync(entities, tran))
                        {
                            tran.Commit();
                            await ClearAccountPermissionCache(model.RoleId);

                            return ResultModel.Success();
                        }
                    }
                    else
                    {
                        tran.Commit();
                        await ClearAccountPermissionCache(model.RoleId);

                        return ResultModel.Success();
                    }
                }
            }

            return ResultModel.Failed();

            #endregion
        }

        public async Task<IResultModel> Select()
        {
            var all = await _repository.GetAllAsync();
            var list = all.Select(m => new OptionResultModel
            {
                Label = m.Name,
                Value = m.Id
            }).ToList();

            return ResultModel.Success(list);
        }

        public Task<bool> AddSpecified(RoleAddModel model)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 清除角色关联账户的权限缓存
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        private async Task ClearAccountPermissionCache(Guid roleId)
        {
            var relationList = await _accountRoleRepository.QueryByRole(roleId);
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
