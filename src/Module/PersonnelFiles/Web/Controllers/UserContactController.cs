using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.UserContactService;
using Nm.Module.PersonnelFiles.Application.UserContactService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserContact.Models;

namespace Nm.Module.PersonnelFiles.Web.Controllers
{
    [Description("用户联系信息管理")]
    public class UserContactController : ModuleController
    {
        private readonly IUserContactService _service;

        public UserContactController(IUserContactService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] UserContactQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(UserContactAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired] Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired] Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(UserContactUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
