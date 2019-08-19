using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Nm.Lib.Cache.Abstractions;
using Nm.Lib.Utils.Core.Encrypt;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService.ResultModels;
using Nm.Module.Admin.Application.AccountService.ViewModels;
using Nm.Module.Admin.Application.SystemService;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Admin.Domain.Account.Models;
using Nm.Module.Admin.Domain.AccountRole;
using Nm.Module.Admin.Domain.Button;
using Nm.Module.Admin.Domain.Menu;
using Nm.Module.Admin.Domain.Permission;
using Nm.Module.Admin.Domain.Role;
using Nm.Module.Admin.Infrastructure;

namespace Nm.Module.Admin.Application.AccountService
{
    public class AccountService : IAccountService
    {
        //默认密码
        public const string DefaultPassword = "123456";
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly DrawingHelper _drawingHelper;
        private readonly ISystemService _systemService;

        public AccountService(ICacheHandler cache, IMapper mapper, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IMenuRepository menuRepository, IRoleRepository roleRepository, IButtonRepository buttonRepository, IPermissionRepository permissionRepository, DrawingHelper drawingHelper, ISystemService systemService)
        {
            _cache = cache;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _menuRepository = menuRepository;
            _roleRepository = roleRepository;
            _buttonRepository = buttonRepository;
            _permissionRepository = permissionRepository;
            _drawingHelper = drawingHelper;
            _systemService = systemService;
        }

        public IResultModel CreateVerifyCode(int length = 6)
        {
            var verifyCodeModel = new VerifyCodeResultModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Base64String = _drawingHelper.DrawVerifyCodeBase64String(out string code, length)
            };

            var key = CacheKeys.VerifyCodeKey + verifyCodeModel.Id;
            //把验证码放到内存缓存中，有效期10分钟
            _cache.SetAsync(key, code, 10);

            return ResultModel.Success(verifyCodeModel);
        }

        public async Task<ResultModel<AccountEntity>> Login(LoginModel model)
        {
            var result = new ResultModel<AccountEntity>();

            var verifyCodeKey = CacheKeys.VerifyCodeKey + model.PictureId;
            var systemConfig = (await _systemService.GetConfig()).Data;
            if (systemConfig.LoginVerifyCode)
            {
                if (model.Code.IsNull())
                    return result.Failed("请输入验证码");

                var code = await _cache.GetAsync(verifyCodeKey);
                if (model.PictureId.IsNull() || !model.Code.Equals(code))
                    return result.Failed("验证码有误");
            }

            var account = await _accountRepository.GetByUserName(model.UserName);
            if (!CheckAccount(account, out string msg))
            {
                return result.Failed(msg);
            }

            var password = EncryptPassword(account.UserName.ToLower(), model.Password);
            if (!account.Password.Equals(password))
                return result.Failed("密码错误");

            #region ==修改登录信息==

            //是否激活
            var status = account.Status == AccountStatus.Inactive ? AccountStatus.Enabled : AccountStatus.UnKnown;
            await _accountRepository.UpdateLoginInfo(account.Id, model.IP, status);

            #endregion

            //删除验证码缓存
            await _cache.RemoveAsync(verifyCodeKey);

            return result.Success(account);
        }

        public async Task<IResultModel> LoginInfo(Guid accountId)
        {
            var account = await _accountRepository.GetAsync(accountId);
            if (!CheckAccount(account, out string msg))
            {
                return ResultModel.Failed(msg);
            }

            var model = new LoginResultModel
            {
                Id = account.Id,
                Name = account.Name,
                Skin = new SkinConfigModel
                {
                    Name = "pretty",
                    Theme = "",
                    FontSize = ""
                }
            };

            var getMenuTree = GetAccountMenuTree(accountId);
            var getButtonCodeList = _buttonRepository.QueryCodeByAccount(accountId);

            model.Menus = await getMenuTree;
            model.Buttons = await getButtonCodeList;

            return ResultModel.Success(model);
        }

        /// <summary>
        /// 检测账户
        /// </summary>
        /// <returns></returns>
        private bool CheckAccount(AccountEntity account, out string msg)
        {
            msg = "";
            if (account == null || account.Deleted)
            {
                msg = "账户不存在";
                return false;
            }
            if (account.Status == AccountStatus.Closed)
            {
                msg = "该账户已注销，请联系管理员~";
                return false;
            }

            if (account.Status == AccountStatus.Disabled)
            {
                msg = "该账户已禁用，请联系管理员~";
                return false;
            }

            return true;
        }

        public async Task<IResultModel> UpdatePassword(UpdatePasswordModel model)
        {
            var account = await _accountRepository.GetAsync(model.AccountId);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var oldPassword = EncryptPassword(account.UserName, model.OldPassword);
            if (!account.Password.Equals(oldPassword))
                return ResultModel.Failed("原密码错误");

            var newPassword = EncryptPassword(account.UserName, model.NewPassword);
            var result = await _accountRepository.UpdatePassword(model.AccountId, newPassword);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> BindRole(AccountRoleBindModel model)
        {
            var exists = await _accountRepository.ExistsAsync(model.AccountId);
            if (!exists)
                return ResultModel.Failed("账户不存在");

            exists = await _roleRepository.ExistsAsync(model.RoleId);
            if (!exists)
                return ResultModel.Failed("角色不存在");

            //添加
            if (model.Checked)
            {
                exists = await _accountRoleRepository.Exists(model.AccountId, model.RoleId);
                if (!exists)
                {
                    var result = await _accountRoleRepository.AddAsync(new AccountRoleEntity { AccountId = model.AccountId, RoleId = model.RoleId });
                    return ResultModel.Result(result);
                }

                return ResultModel.Success();

            }
            {
                //删除
                var result = await _accountRoleRepository.Delete(model.AccountId, model.RoleId);
                return ResultModel.Result(result);
            }
        }

        public async Task<IResultModel> Query(AccountQueryModel model)
        {
            var result = new QueryResultModel<AccountEntity>
            {
                Rows = await _accountRepository.Query(model),
                Total = model.TotalCount
            };

            foreach (var item in result.Rows)
            {
                var roles = await _accountRoleRepository.QueryRole(item.Id);
                item.Roles = roles.Select(r => new OptionResultModel { Label = r.Name, Value = r.Id }).ToList();
            }

            return ResultModel.Success(result);
        }

        public async Task<IResultModel<Guid>> Add(AccountAddModel model)
        {
            var result = new ResultModel<Guid>();

            var account = _mapper.Map<AccountEntity>(model);

            var exists = await Exists(account);
            if (!exists.Successful)
                return exists;

            //默认未激活状态，用户首次登录激活
            account.Status = AccountStatus.Inactive;

            //设置默认密码
            if (account.Password.IsNull())
                account.Password = DefaultPassword;

            account.Password = EncryptPassword(account.UserName.ToLower(), account.Password);

            using (var tran = _accountRepository.BeginTransaction())
            {
                if (await _accountRepository.AddAsync(account, tran))
                {
                    if (model.Roles != null && model.Roles.Any())
                    {
                        var accountRoleList = model.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                        if (await _accountRoleRepository.AddAsync(accountRoleList, tran))
                        {
                            tran.Commit();
                            return result.Success(account.Id);
                        }
                    }
                    else
                    {
                        tran.Commit();
                        return result.Success(account.Id);
                    }
                }
            }

            return result.Failed();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("账户不存在");

            var model = _mapper.Map<AccountUpdateModel>(entity);
            var roles = await _accountRoleRepository.QueryRole(id);
            model.Roles = roles.Select(m => m.Id).ToList();
            return ResultModel.Success(model);
        }

        public async Task<IResultModel> Update(AccountUpdateModel model)
        {
            var entity = await _accountRepository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.Failed("账户不存在！");

            var account = _mapper.Map(model, entity);

            var exists = await Exists(account);
            if (!exists.Successful)
                return exists;

            using (var tran = _accountRepository.BeginTransaction())
            {
                var result = await _accountRepository.UpdateAsync(account, tran);
                if (result)
                {
                    result = await _accountRoleRepository.DeleteByAccount(account.Id, tran);
                    if (result)
                    {
                        if (model.Roles != null && model.Roles.Any())
                        {
                            var accountRoleList = model.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                            if (await _accountRoleRepository.AddAsync(accountRoleList, tran))
                            {
                                tran.Commit();
                                ClearPermissionListCache(account.Id);

                                return ResultModel.Success();
                            }
                        }
                        else
                        {
                            tran.Commit();
                            ClearPermissionListCache(account.Id);

                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id, Guid deleter)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.NotExists;
            if (entity.Id == deleter)
                return ResultModel.Failed("不允许删除自己的账户");

            var result = await _accountRepository.SoftDeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> ResetPassword(Guid id)
        {
            var account = await _accountRepository.GetAsync(id);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var newPassword = EncryptPassword(account.UserName, DefaultPassword);
            var result = await _accountRepository.UpdatePassword(id, newPassword);

            return ResultModel.Result(result);
        }

        public async Task<List<PermissionEntity>> QueryPermissionList(Guid id)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity == null)
                return new List<PermissionEntity>();

            var key = CacheKeys.AccountPermissionListKey + id;

            if (!_cache.TryGetValue(key, out List<PermissionEntity> list))
            {
                list = (await _permissionRepository.QueryByAccount(id)).ToList();
                await _cache.SetAsync(key, list);
            }

            return list;
        }

        public void ClearPermissionListCache(Guid id)
        {
            _cache.RemoveAsync(CacheKeys.AccountPermissionListKey + id).Wait();
        }

        #region ==获取账户的菜单树==

        /// <summary>
        /// 获取账户的菜单树
        /// </summary>
        /// <returns></returns>
        private async Task<List<AccountMenuItem>> GetAccountMenuTree(Guid accountId)
        {
            var entities = (await _menuRepository.GetByAccount(accountId)).Distinct(new MenuComparer()).ToList();
            var all = _mapper.Map<List<AccountMenuItem>>(entities);
            var tree = all.Where(e => e.ParentId.IsEmpty()).OrderBy(e => e.Sort).ToList();

            tree.ForEach(menu =>
            {
                if (menu.Type == MenuType.Node)
                    SetChildren(menu, all);
            });

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

        #endregion

        /// <summary>
        /// 判断账户是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<IResultModel<Guid>> Exists(AccountEntity entity)
        {
            var result = new ResultModel<Guid>();

            if (await _accountRepository.ExistsUserName(entity.UserName, entity.Id, entity.Type))
                return result.Failed("用户名已存在");
            if (entity.Phone.NotNull() && await _accountRepository.ExistsPhone(entity.Phone, entity.Id, entity.Type))
                return result.Failed("手机号已存在");
            if (entity.Email.NotNull() && await _accountRepository.ExistsEmail(entity.Email, entity.Id, entity.Type))
                return result.Failed("邮箱已存在");

            return result.Success(Guid.Empty);
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <returns></returns>
        public string EncryptPassword(string userName, string password)
        {
            return Md5Encrypt.Encrypt($"{userName}_{password}");
        }
    }

    /// <summary>
    /// 菜单比较器
    /// </summary>
    public class MenuComparer : IEqualityComparer<MenuEntity>
    {
        public bool Equals(MenuEntity x, MenuEntity y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id;
        }

        public int GetHashCode(MenuEntity obj)
        {
            return 1;
        }
    }
}