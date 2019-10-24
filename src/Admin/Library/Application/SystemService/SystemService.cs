using System;
using System.Threading.Tasks;
using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.SystemService.ViewModels;
using Nm.Module.Admin.Domain.Config;
using Nm.Module.Admin.Infrastructure;
using Nm.Module.Admin.Infrastructure.Repositories;

namespace Nm.Module.Admin.Application.SystemService
{
    public class SystemService : ISystemService
    {
        /// <summary>
        /// 系统配置项前缀
        /// </summary>
        private const string SystemConfigPrefix = "sys_";

        private readonly IConfigRepository _configRepository;
        private readonly ICacheHandler _cache;
        private readonly AdminDbContext _dbContext;

        public SystemService(IConfigRepository configRepository, ICacheHandler cache, AdminDbContext dbContext)
        {
            _configRepository = configRepository;
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task<IResultModel<SystemConfigModel>> GetConfig(string host = null)
        {
            var result = new ResultModel<SystemConfigModel>();
            if (!_cache.TryGetValue(CacheKeys.SystemConfigCacheKey, out SystemConfigModel model))
            {
                model = new SystemConfigModel();

                var configList = await _configRepository.QueryByPrefix(SystemConfigPrefix);

                foreach (var config in configList)
                {
                    switch (config.Key)
                    {
                        case SystemConfigKey.Title:
                            model.Title = config.Value;
                            break;
                        case SystemConfigKey.Logo:
                            model.Logo = config.Value;
                            break;
                        case SystemConfigKey.Home:
                            model.Home = config.Value;
                            break;
                        case SystemConfigKey.UserInfoPage:
                            model.UserInfoPage = config.Value;
                            break;
                        case SystemConfigKey.ButtonPermission:
                            model.ButtonPermission = config.Value.ToBool();
                            break;
                        case SystemConfigKey.PermissionValidate:
                            model.PermissionValidate = config.Value.ToBool();
                            break;
                        case SystemConfigKey.Auditing:
                            model.Auditing = config.Value.ToBool();
                            break;
                        case SystemConfigKey.LoginVerifyCode:
                            model.LoginVerifyCode = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarFullscreen:
                            model.Toolbar.Fullscreen = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarSkin:
                            model.Toolbar.Skin = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarLogout:
                            model.Toolbar.Logout = config.Value.ToBool();
                            break;
                        case SystemConfigKey.ToolbarUserInfo:
                            model.Toolbar.UserInfo = config.Value.ToBool();
                            break;
                        case SystemConfigKey.CustomCss:
                            model.CustomCss = config.Value;
                            break;
                    }
                }

                await _cache.SetAsync(CacheKeys.SystemConfigCacheKey, model);
            }

            if (host.NotNull() && model.Logo.NotNull())
            {
                model.LogoUrl = new Uri($"{host}/upload/{model.Logo}").ToString().ToLower();
            }

            return result.Success(model);
        }

        public IResultModel UpdateConfig(SystemConfigModel model)
        {
            if (model == null)
                return ResultModel.Failed();

            using (var uow = _dbContext.NewUnitOfWork())
            {
                Update(SystemConfigKey.Title, model.Title, "系统标题", uow).Wait();
                Update(SystemConfigKey.Logo, model.Logo, "系统Logo", uow).Wait();
                Update(SystemConfigKey.Home, model.Home, "系统首页", uow).Wait();
                Update(SystemConfigKey.UserInfoPage, model.UserInfoPage, "个人信息页", uow).Wait();
                Update(SystemConfigKey.ButtonPermission, model.ButtonPermission, "启用按钮权限", uow).Wait();
                Update(SystemConfigKey.PermissionValidate, model.PermissionValidate, "启用权限验证功能", uow).Wait();
                Update(SystemConfigKey.Auditing, model.Auditing, "启用审计日志", uow).Wait();
                Update(SystemConfigKey.LoginVerifyCode, model.LoginVerifyCode, "启用登录验证码功能", uow).Wait();
                Update(SystemConfigKey.ToolbarFullscreen, model.Toolbar.Fullscreen, "显示工具栏全屏按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarSkin, model.Toolbar.Skin, "显示工具栏皮肤按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarLogout, model.Toolbar.Logout, "显示工具栏退出按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarUserInfo, model.Toolbar.UserInfo, "显示工具栏用户信息按钮", uow).Wait();
                Update(SystemConfigKey.CustomCss, model.CustomCss, "自定义CSS样式", uow).Wait();

                uow.Commit();
            }

            _cache.RemoveAsync(CacheKeys.SystemConfigCacheKey).Wait();

            return ResultModel.Success();
        }

        private Task<bool> Update(string key, object value, string remarks, IUnitOfWork uow)
        {
            return _configRepository.UpdateAsync(new ConfigEntity
            {
                Key = key,
                Value = value != null ? value.ToString() : string.Empty,
                Remarks = remarks
            }, uow);
        }
    }
}
