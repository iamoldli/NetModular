using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Models;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.MenuService.ResultModels;
using NetModular.Module.Admin.Application.MenuService.ViewModels;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.ButtonPermission;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.Menu.Models;
using NetModular.Module.Admin.Domain.MenuPermission;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Domain.RoleMenuButton;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.MenuService
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
        private readonly ILogger _logger;
        private readonly AdminDbContext _dbContext;

        public MenuService(IMenuRepository menuRepository, IMenuPermissionRepository menuPermissionRepository, IMapper mapper, IRoleMenuRepository roleMenuRepository, IPermissionRepository permissionRepository, IButtonRepository buttonRepository, IRoleMenuButtonRepository roleMenuButtonRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService, IButtonPermissionRepository buttonPermissionRepository, ILogger<MenuService> logger, AdminDbContext dbContext)
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
            _logger = logger;
            _dbContext = dbContext;
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

            using (var uow = _dbContext.NewUnitOfWork())
            {
                if (await _menuRepository.AddAsync(menu, uow))
                {
                    //同步菜单权限
                    if (await SyncPermission(menu, model.Permissions, uow))
                    {
                        //同步按钮
                        if (await SyncButton(menu, model.Buttons, uow))
                        {
                            uow.Commit();

                            await ClearAccountPermissionCache(menu);

                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            var entity = await _menuRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("该节点不存在");

            if (await _menuRepository.ExistsChild(id))
                return ResultModel.Failed($"当前菜单({entity.Name})存在子菜单，请先删除子菜单");

            if (await _roleMenuRepository.ExistsWidthMenu(entity.Id))
                return ResultModel.Failed($"有角色绑定了当前菜单({entity.Name})，请先删除关联信息");

            using (var uow = _dbContext.NewUnitOfWork())
            {
                /*
                 * 删除逻辑
                 * 1、删除菜单
                 * 2、删除该菜单与权限的关联信息
                 */
                if (await _menuRepository.DeleteAsync(id, uow) && await _roleMenuButtonRepository.DeleteByMenu(entity.Id, uow))
                {
                    uow.Commit();

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

            using (var uow = _dbContext.NewUnitOfWork())
            {
                var result = await _menuRepository.UpdateAsync(entity);
                if (result)
                {
                    //同步菜单权限
                    if (await SyncPermission(entity, model.Permissions, uow))
                    {
                        //同步按钮
                        if (await SyncButton(entity, model.Buttons, uow))
                        {
                            await ClearAccountPermissionCache(entity);
                            uow.Commit();
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

            using (var uow = _dbContext.NewUnitOfWork())
            {
                foreach (var option in model.Options)
                {
                    var entity = await _menuRepository.GetAsync(option.Id, uow);
                    if (entity == null)
                    {
                        return ResultModel.Failed();
                    }

                    entity.Sort = option.Sort;
                    if (!await _menuRepository.UpdateAsync(entity, uow))
                    {
                        return ResultModel.Failed();
                    }
                }

                uow.Commit();
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

        #region ==私有方法==

        /// <summary>
        /// 同步权限
        /// </summary>
        /// <returns></returns>
        private async Task<bool> SyncPermission(MenuEntity menu, List<string> permissions, IUnitOfWork uow)
        {
            if (menu.Type != MenuType.Route)
            {
                return true;
            }

            //删除旧数据
            if (await _menuPermissionRepository.DeleteByMenu(menu.RouteName, uow))
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

                return await _menuPermissionRepository.AddAsync(entityList, uow);
            }

            return false;
        }

        /// <summary>
        /// 同步按钮
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="buttons"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private async Task<bool> SyncButton(MenuEntity menu, List<MenuButtonAddModel> buttons, IUnitOfWork uow)
        {
            if (menu.Type != MenuType.Route)
            {
                return true;
            }

            if (buttons == null)
                buttons = new List<MenuButtonAddModel>();

            var oldButtons = await _buttonRepository.QueryByMenu(menu.RouteName, uow);

            #region ==更新以及新增按钮==

            foreach (var newBtn in buttons)
            {
                var oldBtn = oldButtons.FirstOrDefault(m => m.Code.EqualsIgnoreCase(newBtn.Code));
                bool result;
                if (oldBtn != null)
                {
                    oldBtn.Name = newBtn.Text;
                    oldBtn.Icon = newBtn.Icon;
                    result = await _buttonRepository.UpdateAsync(oldBtn, uow);
                }
                else
                {
                    if (await _buttonRepository.ExistsByCode(newBtn.Code.ToLower(), uow))
                    {
                        _logger.LogError($"按钮编码{newBtn.Code}不能重复");
                        result = false;
                    }
                    else
                    {
                        oldBtn = new ButtonEntity
                        {
                            MenuCode = menu.RouteName,
                            Code = newBtn.Code.ToLower(),
                            Icon = newBtn.Icon,
                            Name = newBtn.Text
                        };
                        result = await _buttonRepository.AddAsync(oldBtn, uow);
                    }
                }

                if (!result)
                    return false;

                if (!await SyncButtonPermission(oldBtn, newBtn.Permissions, uow))
                    return false;
            }

            #endregion

            foreach (var oldBtn in oldButtons)
            {
                var isDel = !buttons.Any(m => m.Code.EqualsIgnoreCase(oldBtn.Code));
                if (isDel)
                {
                    if (!await _buttonRepository.DeleteAsync(oldBtn.Id, uow) || !await _buttonPermissionRepository.DeleteByButton(oldBtn.Code, uow))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 同步按钮权限
        /// </summary>
        /// <param name="button"></param>
        /// <param name="permissions"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        private async Task<bool> SyncButtonPermission(ButtonEntity button, List<string> permissions, IUnitOfWork uow)
        {
            //删除旧数据
            if (await _buttonPermissionRepository.DeleteByButton(button.Code, uow))
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

                if (await _buttonPermissionRepository.AddAsync(entityList, uow))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
