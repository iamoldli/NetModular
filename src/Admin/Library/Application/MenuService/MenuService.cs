using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Config.Abstractions.Impl;
using NetModular.Lib.Utils.Core.Models;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.MenuService.ResultModels;
using NetModular.Module.Admin.Application.MenuService.ViewModels;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.Menu.Models;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.MenuService
{
    public class MenuService : IMenuService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IAccountService _accountService;
        private readonly AdminDbContext _dbContext;
        private readonly IConfigProvider _configProvider;
        private readonly ICacheHandler _cacheHandler;

        public MenuService(IMenuRepository menuRepository, IMapper mapper, IRoleMenuRepository roleMenuRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService, AdminDbContext dbContext, IConfigProvider configProvider, ICacheHandler cacheHandler)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
            _roleMenuRepository = roleMenuRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountService = accountService;
            _dbContext = dbContext;
            _configProvider = configProvider;
            _cacheHandler = cacheHandler;
        }

        public async Task<IResultModel> GetTree()
        {
            var config = _configProvider.Get<SystemConfig>();
            var all = await _menuRepository.GetAllAsync();
            var root = new TreeResultModel<Guid, MenuTreeResultModel>
            {
                Id = Guid.Empty,
                Label = config.Title,
                Item = new MenuTreeResultModel()
            };
            root.Path.Add(root.Label);
            root.Children = ResolveTree(all, root);
            return ResultModel.Success(root);
        }

        /// <summary>
        /// 解析菜单树
        /// </summary>
        private List<TreeResultModel<Guid, MenuTreeResultModel>> ResolveTree(IList<MenuEntity> all, TreeResultModel<Guid, MenuTreeResultModel> parent)
        {
            return all.Where(m => m.ParentId == parent.Id).OrderBy(m => m.Sort)
                .Select(m =>
                {
                    var menu = new TreeResultModel<Guid, MenuTreeResultModel>
                    {
                        Id = m.Id,
                        Label = m.Name,
                        Item = _mapper.Map<MenuTreeResultModel>(m)
                    };
                    menu.Path.AddRange(parent.Path);
                    menu.Path.Add(m.Name);
                    menu.Children = ResolveTree(all, menu);
                    return menu;
                }).ToList();
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

            if (await _menuRepository.AddAsync(menu))
            {
                await ClearAccountPermissionCache(menu);

                return ResultModel.Success(menu.Id);
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

            if (await _menuRepository.DeleteAsync(id))
            {
                return ResultModel.Success();
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
            var result = await _menuRepository.UpdateAsync(entity);
            if (result)
            {
                await ClearAccountPermissionCache(entity);
                return ResultModel.Success();
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

                //清除账户菜单缓存
                await _cacheHandler.RemoveByPrefixAsync(CacheKeys.ACCOUNT_MENUS);

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
                        await _accountService.ClearPermissionListCache(relation.AccountId);
                    }
                }
            }
        }
    }
}
