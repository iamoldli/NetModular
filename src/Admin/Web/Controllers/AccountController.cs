using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.AccountService;
using NetModular.Module.Admin.Application.AccountService.ViewModels;
using NetModular.Module.Admin.Domain.Account.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("账户管理")]
    public class AccountController : ModuleController
    {
        private readonly ILoginInfo _loginInfo;
        private readonly IAccountService _service;

        public AccountController(IAccountService accountService, ILoginInfo loginInfo)
        {
            _service = accountService;
            _loginInfo = loginInfo;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]AccountQueryModel model)
        {
            return _service.Query(model);
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

        [HttpPost]
        [Description("重置密码")]
        public Task<IResultModel> ResetPassword([BindRequired]Guid id)
        {
            return _service.ResetPassword(id);
        }

        [HttpPost]
        [Description("皮肤修改")]
        [Common]
        public Task<IResultModel> SkinUpdate(AccountSkinUpdateModel model)
        {
            return _service.SkinUpdate(_loginInfo.AccountId, model);
        }
    }
}
