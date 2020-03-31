using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Cache.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account;
using NetModular.Module.Admin.Domain.Account.Models;
using NetModular.Module.Admin.Domain.AccountConfig;
using NetModular.Module.Admin.Domain.AccountRole;
using NetModular.Module.Admin.Domain.Permission;
using NetModular.Module.Admin.Domain.Role;
using NetModular.Module.Admin.Infrastructure;
using NetModular.Module.Admin.Infrastructure.PasswordHandler;
using NetModular.Module.Admin.Infrastructure.Repositories;

namespace NetModular.Module.Admin.Application.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly ICacheHandler _cache;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountConfigRepository _accountConfigRepository;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly AdminDbContext _dbContext;
        private readonly IPasswordHandler _passwordHandler;
        private readonly AdminOptions _options;

        public AccountService(ICacheHandler cache, IMapper mapper, IAccountRepository accountRepository, IAccountRoleRepository accountRoleRepository, IRoleRepository roleRepository, IPermissionRepository permissionRepository, IAccountConfigRepository accountConfigRepository, AdminDbContext dbContext, IPasswordHandler passwordHandler, AdminOptions options)
        {
            _cache = cache;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _accountRoleRepository = accountRoleRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _accountConfigRepository = accountConfigRepository;
            _dbContext = dbContext;
            _passwordHandler = passwordHandler;
            _options = options;
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

        public async Task<IResultModel<Guid>> Add(AccountAddModel model, IUnitOfWork uow = null)
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
            {
                if (_options != null && _options.DefaultPassword.NotNull())
                {
                    account.Password = _options.DefaultPassword;
                }
                else
                {
                    account.Password = "123456";
                }
            }

            account.Password = _passwordHandler.Encrypt(account.UserName, account.Password);

            //如果uow参数为空，需要自动处理工作单元
            var noUow = uow == null;
            if (noUow)
            {
                uow = _dbContext.NewUnitOfWork();
            }

            if (await _accountRepository.AddAsync(account, uow))
            {
                if (model.Roles != null && model.Roles.Any())
                {
                    var accountRoleList = model.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                    if (await _accountRoleRepository.AddAsync(accountRoleList, uow))
                    {
                        if (noUow)
                            uow.Commit();

                        return result.Success(account.Id);
                    }
                }
                else
                {
                    if (noUow)
                        uow.Commit();

                    return result.Success(account.Id);
                }
            }

            return result.Failed();
        }

        public async Task<IResultModel> Edit(Guid id)
        {
            var entity = await _accountRepository.GetAsync(id);
            if (entity == null)
                return ResultModel.Failed("账户不存在");
            if (entity.IsLock)
                return ResultModel.Failed("账户锁定，不允许修改");

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
            if (entity.IsLock)
                return ResultModel.Failed("账户锁定，不允许修改");

            var account = _mapper.Map(model, entity);

            var exists = await Exists(account);
            if (!exists.Successful)
                return exists;

            using (var uow = _dbContext.NewUnitOfWork())
            {
                var result = await _accountRepository.UpdateAsync(account, uow);
                if (result)
                {
                    result = await _accountRoleRepository.DeleteByAccount(account.Id, uow);
                    if (result)
                    {
                        if (model.Roles != null && model.Roles.Any())
                        {
                            var accountRoleList = model.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                            result = await _accountRoleRepository.AddAsync(accountRoleList, uow);
                        }

                        if (result)
                        {
                            uow.Commit();
                            ClearPermissionListCache(account.Id);

                            await ClearCache(true, entity.Id);

                            return ResultModel.Success();
                        }
                    }
                }
            }

            return ResultModel.Failed();
        }

        public async Task<IResultModel> Delete(Guid id, Guid deleter)
        {
            var account = await Get(id);
            if (account == null || account.Deleted)
                return ResultModel.NotExists;
            if (account.Id == deleter)
                return ResultModel.Failed("不允许删除自己的账户");
            if (account.IsLock)
                return ResultModel.Failed("账户锁定，不允许删除");

            var result = await _accountRepository.SoftDeleteAsync(id);
            await ClearCache(result, account.Id);

            return ResultModel.Result(result);
        }

        public async Task<IResultModel> UpdatePassword(UpdatePasswordModel model)
        {
            var account = await Get(model.AccountId);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var oldPassword = _passwordHandler.Encrypt(account.UserName, model.OldPassword);
            if (!account.Password.Equals(oldPassword))
                return ResultModel.Failed("原密码错误");

            var newPassword = _passwordHandler.Encrypt(account.UserName, model.NewPassword);
            var result = await _accountRepository.UpdatePassword(model.AccountId, newPassword);

            await ClearCache(result, account.Id);
            return ResultModel.Result(result);
        }

        public async Task<IResultModel> BindRole(AccountRoleBindModel model)
        {
            var account = await Get(model.AccountId);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");

            var exists = await _roleRepository.ExistsAsync(model.RoleId);
            if (!exists)
                return ResultModel.Failed("角色不存在");

            //添加
            if (model.Checked)
            {
                exists = await _accountRoleRepository.Exists(model.AccountId, model.RoleId);
                if (!exists)
                {
                    var result = await _accountRoleRepository.AddAsync(new AccountRoleEntity { AccountId = model.AccountId, RoleId = model.RoleId });
                    await ClearCache(result, account.Id);
                    return ResultModel.Result(result);
                }
                await ClearCache(true, account.Id);

                return ResultModel.Success();
            }
            {
                //删除
                var result = await _accountRoleRepository.Delete(model.AccountId, model.RoleId);
                await ClearCache(result, account.Id);
                return ResultModel.Result(result);
            }
        }

        public async Task<IResultModel> ResetPassword(Guid id)
        {
            var account = await Get(id);
            if (account == null || account.Deleted)
                return ResultModel.Failed("账户不存在");
            if (account.IsLock)
                return ResultModel.Failed("账户锁定，不允许重置密码");

            if (_options != null && _options.DefaultPassword.NotNull())
            {
                account.Password = _options.DefaultPassword;
            }
            else
            {
                account.Password = "123456";
            }

            var newPassword = _passwordHandler.Encrypt(account.UserName, account.Password);
            var result = await _accountRepository.UpdatePassword(id, newPassword);
            await ClearCache(result, account.Id);

            return ResultModel.Result(result);
        }

        public async Task<IList<string>> QueryPermissionList(Guid id, Platform platform)
        {
            var account = await Get(id);
            if (account == null || account.Deleted)
                return new List<string>();

            var key = $"{CacheKeys.ACCOUNT_PERMISSIONS}:{id}:{platform.ToInt()}";

            if (!_cache.TryGetValue(key, out IList<string> list))
            {
                list = await _permissionRepository.QueryCodeByAccount(id, platform);
                await _cache.SetAsync(key, list);
            }

            return list;
        }

        public void ClearPermissionListCache(Guid id)
        {
            var list = EnumExtensions.ToResult<Platform>();
            foreach (var option in list)
            {
                _cache.RemoveAsync($"{CacheKeys.ACCOUNT_PERMISSIONS}:{id}:{option.Value.ToInt()}").Wait();
            }
        }

        public async Task<IResultModel> SkinUpdate(Guid id, AccountSkinUpdateModel model)
        {
            var configInfo = await _accountConfigRepository.GetByAccount(id);
            if (configInfo == null)
            {
                configInfo = new AccountConfigEntity
                {
                    AccountId = id,
                    Skin = model.Name,
                    Theme = model.Theme,
                    FontSize = model.FontSize
                };

                if (await _accountConfigRepository.AddAsync(configInfo))
                    return ResultModel.Success();
            }
            else
            {
                configInfo.Skin = model.Name;
                configInfo.Theme = model.Theme;
                configInfo.FontSize = model.FontSize;

                if (await _accountConfigRepository.UpdateAsync(configInfo))
                    return ResultModel.Success();
            }

            return ResultModel.Failed();
        }

        public async Task<AccountEntity> Get(Guid id)
        {
            var key = $"{CacheKeys.ACCOUNT}:{id}";
            if (_cache.TryGetValue(key, out AccountEntity account))
                return account;

            account = await _accountRepository.GetAsync(id);
            if (account != null)
            {
                await _cache.SetAsync(key, account);
            }

            return account;
        }

        public async Task<IResultModel> Sync(AccountSyncModel model)
        {
            var entity = await _accountRepository.GetAsync(model.Id);
            if (entity == null)
                return ResultModel.Failed("账户不存在！");

            var account = _mapper.Map(model, entity);

            var exists = await Exists(account);
            if (!exists.Successful)
                return exists;

            if (model.NewPassword.NotNull())
            {
                account.Password = _passwordHandler.Encrypt(account.UserName, model.NewPassword);
            }

            using (var uow = _dbContext.NewUnitOfWork())
            {
                var result = await _accountRepository.UpdateAsync(account, uow);
                if (result)
                {
                    if (model.Roles != null)
                    {
                        result = await _accountRoleRepository.DeleteByAccount(account.Id, uow);
                    }
                    if (model.Roles != null && model.Roles.Any())
                    {
                        var accountRoleList = model.Roles.Select(m => new AccountRoleEntity { AccountId = account.Id, RoleId = m }).ToList();
                        result = await _accountRoleRepository.AddAsync(accountRoleList, uow);
                    }

                    if (result)
                    {
                        uow.Commit();

                        ClearPermissionListCache(account.Id);

                        await ClearCache(true, entity.Id);

                        return ResultModel.Success();
                    }
                }
            }

            return ResultModel.Failed();
        }

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
        /// 清除缓存
        /// </summary>
        /// <param name="result"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task ClearCache(bool result, Guid id)
        {
            if (result)
            {
                return _cache.RemoveAsync($"{CacheKeys.ACCOUNT}:{id}");
            }

            return Task.CompletedTask;
        }
    }
}