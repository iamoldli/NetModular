using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Encrypt;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService.ResultModels;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Button;
using NetModular.Module.Admin.Domain.Menu;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.AccountService
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// 验证码缓存Key
        /// </summary>
        public const string VerifyCodeKey = "ADMIN_VERIFY_CODE_";

        /// <summary>
        /// 账户权限列表缓存Key
        /// </summary>
        public const string AccountPermissionListKey = "ADMIN_ACCOUNT_PERMISSION_LIST_";

        //默认密码
        public const string DefaultPassword = "123456";
        private readonly LoginInfo _loginInfo;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IButtonRepository _buttonRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly DrawingHelper _drawingHelper;
        private readonly ILogger _logger;

        public AccountService(LoginInfo loginInfo, IMemoryCache cache, IMapper mapper, IUnitOfWork<AdminDbContext> uow, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IMenuRepository menuRepository, IRoleRepository roleRepository, IButtonRepository buttonRepository, IPermissionRepository permissionRepository, DrawingHelper drawingHelper, ILogger<AccountService> logger)
        {
            _loginInfo = loginInfo;
            _cache = cache;
            _mapper = mapper;
            _uow = uow;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _menuRepository = menuRepository;
            _roleRepository = roleRepository;
            _buttonRepository = buttonRepository;
            _permissionRepository = permissionRepository;
            _drawingHelper = drawingHelper;
            _logger = logger;
        }

        public IResultModel CreateVerifyCode(int length = 6)
        {
            var verifyCodeModel = new VerifyCodeResultModel
            {
                Id = Guid.NewGuid().ToString("N"),
                Base64String = _drawingHelper.DrawVerifyCodeBase64String(out string code, length)
            };

            //把验证码放到内存缓存中，有效期10分钟
            _cache.Set(VerifyCodeKey + verifyCodeModel.Id, code, new TimeSpan(0, 10, 0));

            return ResultModel.Success(verifyCodeModel);
        }

        public async Task<ResultModel<Account>> Login(LoginModel model)
        {
            var result = new ResultModel<Account>();
            var verifyCodeKey = VerifyCodeKey + model.PictureId;
            if (model.PictureId.IsNull() || !model.Code.Equals(_cache.Get(verifyCodeKey)))
                return result.Failed("验证码有误");

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
            await _accountRepository.UpdateLoginInfo(account.Id, _loginInfo.IPv4, status);

            #endregion

            //删除验证码缓存
            _cache.Remove(verifyCodeKey);

            return result.Success(account);
        }

        public async Task<IResultModel> LoginInfo()
        {
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
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

            var getMenuTree = GetAccountMenuTree();
            var getButtonCodeList = _buttonRepository.QueryCodeByAccount(_loginInfo.AccountId);

            model.Menus = await getMenuTree;
            model.Buttons = await getButtonCodeList;

            return ResultModel.Success(model);
        }

        /// <summary>
        /// 检测账户
        /// </summary>
        /// <returns></returns>
        private bool CheckAccount(Account account, out string msg)
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
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var oldPassword = EncryptPassword(account.UserName, model.OldPassword);
            if (!account.Password.Equals(oldPassword))
                return ResultModel.Failed("原密码错误");

            var newPassword = EncryptPassword(account.UserName, model.NewPassword);
            var result = await _accountRepository.UpdatePassword(_loginInfo.AccountId, newPassword);

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
                    var result = await _accountRoleRepository.AddAsync(new AccountRole { AccountId = model.AccountId, RoleId = model.RoleId });
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
            var result = new QueryResultModel<AccountQueryResultModel>();
            var paging = model.Paging();

            var list = await _accountRepository.Query(paging, model.UserName, model.Name, model.Phone, model.Email);
            result.Rows = _mapper.Map<List<AccountQueryResultModel>>(list);
            result.Total = paging.TotalCount;

            foreach (var item in result.Rows)
            {
                var roles = await _accountRoleRepository.QueryRole(item.Id);
                item.Roles = roles.Select(r => new OptionResultModel { Label = r.Name, Value = r.Id }).ToList();
            }

            return ResultModel.Success(result);
        }

        public async Task<IResultModel> Add(AccountAddModel model)
        {
            var account = _mapper.Map<Account>(model);

            var exists = await Exists(account);
            if (!exists.Successful)
                return exists;

            //默认未激活状态，用户首次登录激活
            account.Status = AccountStatus.Inactive;

            //设置默认密码
            if (account.Password.IsNull())
                account.Password = DefaultPassword;

            account.Password = EncryptPassword(account.UserName.ToLower(), account.Password);

            _uow.BeginTransaction();
            var result = await _accountRepository.AddAsync(account);
            if (result)
            {
                if (model.Roles != null && model.Roles.Any())
                {
                    var accountRoleList = model.Roles.Select(m => new AccountRole { AccountId = account.Id, RoleId = m }).ToList();
                    if (await _accountRoleRepository.AddAsync(accountRoleList))
                    {
                        _uow.Commit();
                        return ResultModel.Success();
                    }
                }
                else
                {
                    _uow.Commit();
                    return ResultModel.Success();
                }
            }

            return ResultModel.Failed();
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

            _uow.BeginTransaction();
            var result = await _accountRepository.UpdateAsync(account);
            if (result)
            {
                result = await _accountRoleRepository.DeleteByAccount(account.Id);
                if (result)
                {
                    if (model.Roles != null && model.Roles.Any())
                    {
                        var accountRoleList = model.Roles.Select(m => new AccountRole { AccountId = account.Id, RoleId = m }).ToList();
                        if (await _accountRoleRepository.AddAsync(accountRoleList))
                        {
                            _uow.Commit();
                            ClearPermissionListCache(account.Id);

                            return ResultModel.Success();
                        }
                    }
                    else
                    {
                        _uow.Commit();
                        ClearPermissionListCache(account.Id);

                        return ResultModel.Success();
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id)
        {
            if (!await _accountRepository.ExistsAsync(id))
                return ResultModel.NotExists;

            var result = await _accountRepository.SoftDeleteAsync(id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> ResetPassword(Guid id)
        {
            var account = await _accountRepository.GetAsync(_loginInfo.AccountId);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var newPassword = EncryptPassword(account.UserName, DefaultPassword);
            var result = await _accountRepository.UpdatePassword(_loginInfo.AccountId, newPassword);

            return ResultModel.Result(result);
        }

        public async Task<List<Permission>> QueryPermissionList(Guid id)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity == null)
                return new List<Permission>();

            var key = AccountPermissionListKey + id;
            //TODO:清除账户权限的缓存
            if (!_cache.TryGetValue(key, out List<Permission> list))
            {
                list = (await _permissionRepository.QueryByAccount(id)).ToList();
                _cache.Set(key, list);
            }

            return list;
        }

        public void ClearPermissionListCache(Guid id)
        {
            _cache.Remove(AccountPermissionListKey + id);
        }

        #region ==获取账户的菜单树==

        /// <summary>
        /// 获取账户的菜单树
        /// </summary>
        /// <returns></returns>
        private async Task<List<AccountMenuItem>> GetAccountMenuTree()
        {
            var entities = (await _menuRepository.GetByAccount(_loginInfo.AccountId)).Distinct(new MenuComparer()).ToList();
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
        private async Task<IResultModel> Exists(Account entity)
        {
            if (await _accountRepository.ExistsUserName(entity.UserName, entity.Id))
                return ResultModel.Failed("用户名已存在");
            if (entity.Phone.NotNull() && await _accountRepository.ExistsPhone(entity.Phone, entity.Id))
                return ResultModel.Failed("手机号已存在");
            if (entity.Email.NotNull() && await _accountRepository.ExistsEmail(entity.Email, entity.Id))
                return ResultModel.Failed("邮箱已存在");

            return ResultModel.Success();
        }

        /// <summary>
        /// 密码加密
        /// </summary>
        /// <returns></returns>
        private string EncryptPassword(string userName, string password)
        {
            return Md5Encrypt.Encrypt($"{userName}_{password}");
        }
    }

    /// <summary>
    /// 菜单比较器
    /// </summary>
    public class MenuComparer : IEqualityComparer<Menu>
    {
        public bool Equals(Menu x, Menu y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id;
        }

        public int GetHashCode(Menu obj)
        {
            return 1;
        }
    }
}