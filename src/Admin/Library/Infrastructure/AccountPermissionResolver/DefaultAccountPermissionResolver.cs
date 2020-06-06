using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.RoleButton;
using NetModular.Module.Admin.Domain.RolePage;
using NetModular.Module.Admin.Domain.RolePermission;

namespace NetModular.Module.Admin.Infrastructure.AccountPermissionResolver
{
    [Singleton]
    public class DefaultAccountPermissionResolver : IAccountPermissionResolver
    {
        private readonly ICacheHandler _cache;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IRoleButtonRepository _roleButtonRepository;
        private readonly IRolePageRepository _rolePageRepository;
        private readonly IMenuRepository _menuRepository;

        public DefaultAccountPermissionResolver(ICacheHandler cache, IRolePermissionRepository rolePermissionRepository, IRoleButtonRepository roleButtonRepository, IRolePageRepository rolePageRepository, IMenuRepository menuRepository)
        {
            _cache = cache;
            _rolePermissionRepository = rolePermissionRepository;
            _roleButtonRepository = roleButtonRepository;
            _rolePageRepository = rolePageRepository;
            _menuRepository = menuRepository;
        }

        public async Task<IList<string>> Resolve(Guid accountId, Platform platform)
        {
            if (accountId.IsEmpty())
                return new List<string>();

            var key = $"{CacheKeys.ACCOUNT_PERMISSIONS}{accountId}:{platform.ToInt()}";

            if (!_cache.TryGetValue(key, out IList<string> list))
            {
                list = await _rolePermissionRepository.QueryByAccount(accountId, platform);
                await _cache.SetAsync(key, list);
            }

            return list;
        }

        public async Task<IList<AccountMenuItem>> ResolveMenus(Guid accountId)
        {
            var key = $"{CacheKeys.ACCOUNT_MENUS}{accountId}";
            if (!_cache.TryGetValue(key, out List<AccountMenuItem> tree))
            {
                var entities = (await _menuRepository.QueryByAccount(accountId)).Distinct(new MenuComparer()).ToList();
                var all = entities.Select(m => new AccountMenuItem
                {
                    Id = m.Id,
                    ParentId = m.ParentId,
                    ModuleCode = m.ModuleCode,
                    Type = m.Type,
                    Name = m.Name,
                    RouteName = m.RouteName,
                    RouteParams = m.RouteParams,
                    RouteQuery = m.RouteQuery,
                    Icon = m.Icon,
                    DialogFullscreen = m.DialogFullscreen,
                    DialogHeight = m.DialogHeight,
                    DialogWidth = m.DialogWidth,
                    IconColor = m.IconColor,
                    Level = m.Level,
                    Show = m.Show,
                    Target = m.Target,
                    Sort = m.Sort,
                    Url = m.Url
                }).ToList();

                tree = all.Where(e => e.ParentId.IsEmpty()).OrderBy(e => e.Sort).ToList();

                tree.ForEach(menu =>
                {
                    if (menu.Type == MenuType.Node)
                        SetChildren(menu, all);
                });

                await _cache.SetAsync(key, tree);
            }

            return tree;
        }

        private void SetChildren(AccountMenuItem parent, List<AccountMenuItem> all)
        {
            parent.Children = all.Where(e => e.ParentId == parent.Id).OrderBy(e => e.Sort).ToList();

            if (parent.Children.Any())
            {
                parent.Children.ForEach(menu =>
                {
                    if (menu.Type == MenuType.Node)
                        SetChildren(menu, all);
                });
            }
        }

        public async Task<IList<string>> ResolvePages(Guid accountId)
        {
            if (accountId.IsEmpty())
                return new List<string>();

            var key = $"{CacheKeys.ACCOUNT_PAGES}{accountId}";

            if (!_cache.TryGetValue(key, out IList<string> list))
            {
                list = await _rolePageRepository.QueryPageCodesByAccount(accountId);
                await _cache.SetAsync(key, list);
            }

            return list;
        }

        public async Task<IList<string>> ResolveButtons(Guid accountId)
        {
            if (accountId.IsEmpty())
                return new List<string>();

            var key = $"{CacheKeys.ACCOUNT_BUTTONS}{accountId}";

            if (!_cache.TryGetValue(key, out IList<string> list))
            {
                list = await _roleButtonRepository.QueryButtonCodesByAccount(accountId);
                await _cache.SetAsync(key, list);
            }

            return list;
        }
    }
}
