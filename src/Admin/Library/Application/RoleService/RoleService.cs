using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.RoleService.ViewModels;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Domain.Role.Models;
using NetModular.Module.Admin.Domain.RoleButton;
using NetModular.Module.Admin.Domain.RoleMenu;
using NetModular.Module.Admin.Domain.RolePage;
using NetModular.Module.Admin.Domain.RolePermission;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _repository;
        private readonly IRoleMenuRepository _roleMenuRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRolePageRepository _pageRepository;
        private readonly IRoleButtonRepository _buttonRepository;
        private readonly IRolePermissionRepository _permissionRepository;
        private readonly IAccountService _accountService;
        private readonly ICacheHandler _cacheHandler;

        private readonly AdminDbContext _dbContext;

        public RoleService(IMapper mapper, IRoleRepository repository, IRoleMenuRepository roleMenuRepository, IAccountRoleRepository accountRoleRepository, IAccountService accountService, IMenuRepository menuRepository, AdminDbContext dbContext, ICacheHandler cacheHandler, IRolePageRepository pageRepository, IRoleButtonRepository buttonRepository1, IRolePermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _roleMenuRepository = roleMenuRepository;
            _accountRoleRepository = accountRoleRepository;
            _accountService = accountService;
            _menuRepository = menuRepository;
            _dbContext = dbContext;
            _cacheHandler = cacheHandler;
            _pageRepository = pageRepository;
            _buttonRepository = buttonRepository1;
            _permissionRepository = permissionRepository;
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

            using (var uow = _dbContext.NewUnitOfWork())
            {
                var result = await _repository.SoftDeleteAsync(id, uow);
                if (result)
                {
                    result = await _roleMenuRepository.DeleteByRoleId(id, uow);
                    if (result)
                    {
                        uow.Commit();
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

        public async Task<IResultModel> QueryBindPages(Guid roleId)
        {
            if (!await _repository.ExistsAsync(roleId))
                return ResultModel.NotExists;

            var model = new RolePageBindModel
            {
                RoleId = roleId
            };

            var pageCodes = await _pageRepository.QueryPageCodesByRole(roleId);
            var buttons = await _buttonRepository.QueryButtonCodes(roleId);

            if (pageCodes.Any())
            {
                foreach (var pageCode in pageCodes)
                {
                    var page = new PageBindModel
                    {
                        Code = pageCode,
                        Buttons = buttons.Where(m => m.PageCode == pageCode).Select(m => m.ButtonCode).ToList()
                    };

                    model.Pages.Add(page);
                }
            }

            return ResultModel.Success(model);
        }

        public async Task<IResultModel> BindPages(RolePageBindModel model)
        {
            if (!await _repository.ExistsAsync(model.RoleId))
                return ResultModel.NotExists;

            using var uow = _dbContext.NewUnitOfWork();

            if (!await _pageRepository.DeleteByRole(model.RoleId, uow))
            {
                return ResultModel.Failed();
            }
            if (!await _buttonRepository.DeleteByRole(model.RoleId, uow))
            {
                return ResultModel.Failed();
            }
            if (!await _permissionRepository.DeleteByRole(model.RoleId, Platform.Web, uow))
            {
                return ResultModel.Failed();
            }

            if (model.Pages != null && model.Pages.Any())
            {
                var pages = new List<RolePageEntity>();
                var buttons = new List<RoleButtonEntity>();
                var permissions = new List<RolePermissionEntity>();
                foreach (var page in model.Pages)
                {
                    //插入绑定页面
                    pages.Add(new RolePageEntity
                    {
                        PageCode = page.Code,
                        RoleId = model.RoleId
                    });

                    //插入绑定按钮
                    if (page.Buttons != null && page.Buttons.Any())
                    {
                        buttons.AddRange(page.Buttons.Select(button => new RoleButtonEntity
                        {
                            RoleId = model.RoleId,
                            ButtonCode = button,
                            PageCode = page.Code
                        }));
                    }

                    //插入绑定权限
                    if (page.Permissions != null && page.Permissions.Any())
                    {
                        permissions.AddRange(page.Permissions.Select(permission => new RolePermissionEntity
                        {
                            RoleId = model.RoleId,
                            Platform = Platform.Web,
                            PermissionCode = permission
                        }));
                    }
                }
                if (pages.Any())
                {
                    await _pageRepository.AddAsync(pages, uow);
                }
                if (buttons.Any())
                {
                    await _buttonRepository.AddAsync(buttons, uow);
                }
                if (permissions.Any())
                {
                    await _permissionRepository.AddAsync(permissions, uow);
                }
            }

            uow.Commit();

            //清除缓存
            await ClearAccountPermissionCache(model.RoleId);
            return ResultModel.Success();
        }

        public async Task<IResultModel> QueryBindMenus(Guid id)
        {
            if (!await _repository.ExistsAsync(id))
                return ResultModel.NotExists;

            var menus = await _menuRepository.QueryByRole(id);
            var list = menus.Where(m => m.Type != MenuType.Node).Select(m => m.Id).ToList();

            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindMenus(RoleMenuBindModel model)
        {
            if (!await _repository.ExistsAsync(model.RoleId))
                return ResultModel.NotExists;

            using var uow = _dbContext.NewUnitOfWork();

            if (await _roleMenuRepository.DeleteByRoleId(model.RoleId, uow))
            {
                if (model.Menus != null && model.Menus.Any())
                {
                    var menus = model.Menus
                        .Where(menuId => menuId.NotEmpty())
                        .Select(menuId => new RoleMenuEntity
                        {
                            RoleId = model.RoleId,
                            MenuId = menuId
                        }).ToList();
                    await _roleMenuRepository.AddAsync(menus, uow);
                }

                uow.Commit();

                //清楚缓存
                await ClearAccountPermissionCache(model.RoleId);

                return ResultModel.Success();
            }

            return ResultModel.Failed();
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

        public async Task<IResultModel> QueryPlatformPermissions(Guid roleId, Platform platform)
        {
            if (platform == Platform.Web)
                return ResultModel.Failed("WEB平台请使用页面授权");

            var list = await _permissionRepository.QueryByRole(roleId, platform);
            return ResultModel.Success(list);
        }

        public async Task<IResultModel> BindPlatformPermissions(RolePermissionBindModel model)
        {
            if (model.Platform == Platform.Web)
                return ResultModel.Failed("WEB平台请使用页面授权");

            using var uow = _dbContext.NewUnitOfWork();
            var result = await _permissionRepository.DeleteByRole(model.RoleId, model.Platform, uow);
            if (result)
            {
                if (model.Permissions != null && model.Permissions.Any())
                {
                    var list = model.Permissions.Select(m => new RolePermissionEntity
                    {
                        RoleId = model.RoleId,
                        Platform = model.Platform,
                        PermissionCode = m
                    }).ToList();

                    await _permissionRepository.AddAsync(list, uow);
                }

                uow.Commit();
                await ClearAccountPermissionCache(model.RoleId);
                return ResultModel.Success();
            }

            return ResultModel.Failed();
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
                    await _accountService.ClearPermissionListCache(relation.AccountId);
                }
            }
        }
    }
}
