using System;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Auth.Web;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Module.AspNetCore.Attributes;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Admin.Application.AccountService;
using Nm.Module.Admin.Application.AccountService.ViewModels;
using Nm.Module.Admin.Domain.Account.Models;

namespace Nm.Module.Admin.Web.Controllers
{
    [Description("账户管理")]
    public class AccountController : ModuleController
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountService _service;
        private readonly ILoginHandler _loginHandler;
        public AccountController(IAccountService accountService, ILoginHandler loginHandler, ILoginInfo loginInfo)
        {
            _service = accountService;
            _loginHandler = loginHandler;
            _loginInfo = loginInfo;
        }

        [HttpGet]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("获取验证码")]
        public IResultModel VerifyCode(int length = 6)
        {
            return _service.CreateVerifyCode(length);
        }

        [HttpPost]
        [AllowAnonymous]
        [DisableAuditing]
        [Description("登录")]
        public async Task<IResultModel> Login([FromBody]LoginModel model)
        {
            model.IP = _loginInfo.IPv4;
            var result = await _service.Login(model);
            if (result.Successful)
            {
                var account = result.Data;
                var claims = new[]
                {
                    new Claim(ClaimsName.AccountId,account.Id.ToString()),
                    new Claim(ClaimsName.AccountType,model.AccountType.ToInt().ToString()),
                    new Claim(ClaimsName.Platform,model.Platform.ToInt().ToString())
                };

                return _loginHandler.Hand(claims);
            }

            return ResultModel.Failed(result.Msg);
        }

        [HttpGet]
        [DisableAuditing]
        [Common]
        [Description("获取账户登录信息")]
        public Task<IResultModel> LoginInfo()
        {
            return _service.LoginInfo(_loginInfo.AccountId);
        }

        [HttpPost]
        [Description("修改密码")]
        [Common]
        public Task<IResultModel> UpdatePassword(UpdatePasswordModel model)
        {
            model.AccountId = _loginInfo.AccountId;
            return _service.UpdatePassword(model);
        }

        [HttpPost]
        [Description("绑定角色")]
        public Task<IResultModel> BindRole(AccountRoleBindModel model)
        {
            return _service.BindRole(model);
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]AccountQueryModel model)
        {
            return await _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel<Guid>> Add(AccountAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("更新")]
        public Task<IResultModel> Update(AccountUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id, _loginInfo.AccountId);
        }

        [HttpPost]
        [Description("重置密码")]
        public Task<IResultModel> ResetPassword([BindRequired]Guid id)
        {
            return _service.ResetPassword(id);
        }
    }
}
