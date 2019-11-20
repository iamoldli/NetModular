using System;
using System.Threading.Tasks;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.SystemService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.SystemService
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
                        case SystemConfigKey.MenuUniqueOpened:
                            model.MenuUniqueOpened = config.Value.ToBool();
                            break;
                        case SystemConfigKey.DialogCloseOnClickModal:
                            model.DialogCloseOnClickModal = config.Value.ToBool();
                            break;
                        case SystemConfigKey.LoginOptionsType:
                            model.LoginOptions.Type = config.Value;
                            break;
                        case SystemConfigKey.LoginOptionsVerifyCode:
                            model.LoginOptions.VerifyCode = config.Value.ToBool();
                            break;
                        case SystemConfigKey.Copyright:
                            model.Copyright = config.Value;
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
                Update(SystemConfigKey.ToolbarFullscreen, model.Toolbar.Fullscreen, "显示工具栏全屏按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarSkin, model.Toolbar.Skin, "显示工具栏皮肤按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarLogout, model.Toolbar.Logout, "显示工具栏退出按钮", uow).Wait();
                Update(SystemConfigKey.ToolbarUserInfo, model.Toolbar.UserInfo, "显示工具栏用户信息按钮", uow).Wait();
                Update(SystemConfigKey.CustomCss, model.CustomCss, "自定义CSS样式", uow).Wait();
                Update(SystemConfigKey.MenuUniqueOpened, model.MenuUniqueOpened, "菜单只能打开一个", uow).Wait();
                Update(SystemConfigKey.DialogCloseOnClickModal, model.DialogCloseOnClickModal, "点击模态框关闭对话框", uow).Wait();
                Update(SystemConfigKey.LoginOptionsType, model.LoginOptions.Type, "登录页类型", uow).Wait();
                Update(SystemConfigKey.LoginOptionsVerifyCode, model.LoginOptions.VerifyCode, "启用登录验证码功能", uow).Wait();
                Update(SystemConfigKey.Copyright, model.Copyright, "版权申明", uow).Wait();

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
