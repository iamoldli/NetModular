using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.UserWorkHistoryService;
using Nm.Module.PersonnelFiles.Application.UserWorkHistoryService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.UserWorkHistory.Models;

namespace Nm.Module.PersonnelFiles.Web.Controllers
{
    [Description("用户工作经历管理")]
    public class UserWorkHistoryController : ModuleController
    {
        private readonly IUserWorkHistoryService _service;

        public UserWorkHistoryController(IUserWorkHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] UserWorkHistoryQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(UserWorkHistoryAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired] int id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired] int id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(UserWorkHistoryUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
