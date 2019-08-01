using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Utils.Core.Models;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Application.MenuService.ResultModels;
using Nm.Module.Admin.Application.MenuService.ViewModels;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.ButtonPermission;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.Menu.Models;
using Nm.Module.Admin.Domain.MenuPermission;
using Nm.Module.Admin.Domain.Permission;
using Nm.Module.Admin.Domain.RoleMenu;
using Nm.Module.Admin.Domain.RoleMenuButton;

namespace Nm.Module.Admin.Application.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuPermissionRepository _menuPermissionRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly IRoleMenuButtonRepository _roleMenuButtonRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IButtonPermissionRepository _buttonPermissionRepository;
        private readonly IAccountService _accountService;

        public MenuService(IMenuRepository menuRepository, IMenuPermissionRepository menuPermissionRepository, IMapper mapper, IRoleMenuRepository roleMenuRepository, IPermissionRepository permissionRepository, IButtonRepository buttonRepository, IRoleMenuButtonRepository roleMenuButtonRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService, IButtonPermissionRepository buttonPermissionRepository)
        {
            _menuRepository = menuRepository;
            _menuPermissionRepository = menuPermissionRepository;
            _mapper = mapper;
            _roleMenuRepository = roleMenuRepository;
            _buttonRepository = buttonRepository;
            _roleMenuButtonRepository = roleMenuButtonRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountService = accountService;
            _buttonPermissionRepository = buttonPermissionRepository;
        }

        public async Task<IResultModel> GetTree()
        {
            var treeModel = new List<MenuTreeResultModel>();

            var all = await _menuRepository.GetAllAsync();
            var father = all.Where(m => m.ParentId == default(Guid) && m.Level == 0)
                .OrderBy(m => m.Sort).ToList();

            foreach (var menu in father)
            {
                treeModel.Add(Menu2TreeModel(all, menu));
            }

            return ResultModel.Success(treeModel);
        }

        /// <summary>
        /// 菜单对象转换为树模型
        /// </summary>
        /// <param name="all"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        private MenuTreeResultModel Menu2TreeModel(IList<MenuEntity> all, MenuEntity menu)
        {
            var model = _mapper.Map<MenuTreeResultModel>(menu);

            if (!all.Any())
                return model;

            if (menu.Type == MenuType.Link)
                return model;

            var children = all.Where(m => m.ParentId == menu.Id).OrderBy(m => m.Sort).ToList();
            if (children.Any())
            {
                foreach (var child in children)
                {
                    model.Children.Add(Menu2TreeModel(all, child));
                }
            }

            return model;
        }

        #region ==添加菜单==

        public async Task<IResultModel> Add(MenuAddModel model)
        {
            var menu = _mapper.Map<MenuEntity>(model);

            //根据父节点的等级+1设置当前菜单的等级
            if (menu.ParentId != Guid.Empty)
            {
                var parentMenu = await _menuRepository.GetAsync(model.ParentId);
                if (parentMenu == null)
                {
                    return ResultModel.Failed("父节点不存在");
                }

                menu.Level = parentMenu.Level + 1;
            }

            if (menu.Type == MenuType.Node)
                menu.Target = MenuTarget.UnKnown;

            using (var tran = _menuRepository.BeginTransaction())
            {
                if (await _menuRepository.AddAsync(menu, tran))
                {
                    //同步菜单权限
                    if (await SyncPermission(menu, model.Permissions, tran))
                    {
                        //同步按钮
                        if (await SyncButton(menu, model.Buttons, tran))
                        {
                            tran.Commit();
                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        /// <summary>
        /// 同步权限
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SyncPermission(MenuEntity menu, List<string> permissions, IDbTransaction transaction)
        {
            if (menu.Type != MenuType.Route)
            {
                return true;
            }

            //删除旧数据
            if (await _menuPermissionRepository.DeleteByMenu(menu.RouteName, transaction))
            {
                if (permissions == null || !permissions.Any())
                {
                    await ClearAccountPermissionCache(menu);
                    return true;
                }

                //添加新数据
                var entityList = new List<MenuPermissionEntity>();
                permissions.ForEach(code =>
                {
                    entityList.Add(new MenuPermissionEntity { MenuCode = menu.RouteName, PermissionCode = code.ToLower() });
                });

                if (await _menuPermissionRepository.AddAsync(entityList, transaction))
                {
                    await ClearAccountPermissionCache(menu);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 同步按钮
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="buttons"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private async Task<bool> SyncButton(MenuEntity menu, List<MenuButtonAddModel> buttons, IDbTransaction transaction)
        {
            if (menu.Type != MenuType.Route)
            {
                return true;
            }

            //删除旧数据
            if (await _buttonRepository.DeleteByMenu(menu.RouteName, transaction))
            {
                if (buttons == null || !buttons.Any())
                    return true;

                //添加新数据
                foreach (var btn in buttons)
                {
                    var entity = new ButtonEntity
                    {
                        MenuCode = menu.RouteName,
                        Code = btn.Code.ToLower(),
                        Icon = btn.Icon,
                        Name = btn.Text
                    };

                    if (!await _buttonRepository.AddAsync(entity, transaction))
                        return false;

                    if (!await SyncButtonPermission(entity, btn.Permissions, transaction))
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// 同步按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <param name="permissions"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private async Task<bool> SyncButtonPermission(ButtonEntity button, List<string> permissions, IDbTransaction transaction)
        {
            //删除旧数据
            if (await _buttonPermissionRepository.DeleteByButton(button.Code, transaction))
            {
                if (permissions == null || !permissions.Any())
                {
                    return true;
                }

                //添加新数据
                var entityList = new List<ButtonPermissionEntity>();
                permissions.ForEach(code =>
                {
                    entityList.Add(new ButtonPermissionEntity { ButtonCode = button.Code, PermissionCode = code.ToLower() });
                });

                if (await _buttonPermissionRepository.AddAsync(entityList, transaction))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _menuRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("该节点不存在");

            if (await _menuRepository.ExistsChild(id))
                return ResultModel.Failed($"当前菜单({entity.Name})存在子菜单，请先删除子菜单");

            if (await _roleMenuRepository.ExistsWidthMenu(entity.Id))
                return ResultModel.Failed($"有角色绑定了当前菜单({entity.Name})，请先删除关联信息");

            using (var tran = _menuRepository.BeginTransaction())
            {
                /*
                 * 删除逻辑
                 * 1、删除菜单
                 * 2、删除该菜单与权限的关联信息
                 */
                if (await _menuRepository.DeleteAsync(id, tran) && await _roleMenuButtonRepository.DeleteByMenu(entity.Id, tran))
                {
                    tran.Commit();

                    await ClearAccountPermissionCache(entity);

                    return ResultModel.Success();
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _menuRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("菜单不存在");

            var model = _mapper.Map<MenuUpdateModel>(entity);
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(MenuUpdateModel model)
        {
            var entity = await _menuRepository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.Failed("菜单不存在！");

            entity = _mapper.Map(model, entity);

            using (var tran = _menuRepository.BeginTransaction())
            {
                var result = await _menuRepository.UpdateAsync(entity);
                if (result)
                {
                    //同步菜单权限
                    if (await SyncPermission(entity, model.Permissions, tran))
                    {
                        //同步按钮
                        if (await SyncButton(entity, model.Buttons, tran))
                        {
                            tran.Commit();
                            return ResultModel.Success();
                        }
                    }
                }
            }
            return ResultModel.Failed();
        }

        public async Task<IResultModel> Query(MenuQueryModel model)
        {
            var queryResult = new QueryResultModel<MenuEntity>
            {
                Rows = await _menuRepository.Query(model),
                Total = model.TotalCount
            };

            return ResultModel.Success(queryResult);
        }

        public async Task<IResultModel> QuerySortList(Guid parentId)
        {
            var model = new SortUpdateModel<Guid>();
            var all = await _menuRepository.QueryChildren(parentId);
            model.Options = all.Select(m => new SortOptionModel<Guid>()
            {
                Id = m.Id,
                Label = m.Name,
                Sort = m.Sort
            }).ToList();

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> UpdateSortList(SortUpdateModel<Guid> model)
        {
            if (model.Options == null || !model.Options.Any())
            {
                return ResultModel.Failed("不包含数据");
            }

            using (var tran = _menuRepository.BeginTransaction())
            {
                foreach (var option in model.Options)
                {
                    var entity = await _menuRepository.GetAsync(option.Id, tran);
                    if (entity == null)
                    {
                        return ResultModel.Failed();
                    }

                    entity.Sort = option.Sort;
                    if (!await _menuRepository.UpdateAsync(entity, tran))
                    {
                        return ResultModel.Failed();
                    }
                }

                tran.Commit();
            }

            return ResultModel.Success();
        }

        /// <summary>
        /// 清除菜单关联账户的权限缓存
        /// </summary>
        /// <returns></returns>
        private async Task ClearAccountPermissionCache(MenuEntity menu)
        {
            if (menu.Type == MenuType.Route)
            {
                var relationList = await _accountRoleRepository.QueryByMenu(menu.Id);
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
}
