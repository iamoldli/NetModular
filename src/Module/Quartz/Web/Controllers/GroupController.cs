using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Quartz.Application.GroupService;
using Tm.Module.Quartz.Application.GroupService.ViewModels;
using Tm.Module.Quartz.Domain.Group.Models;

namespace Tm.Module.Quartz.Web.Controllers
{
    [Description("任务组管理")]
    public class GroupController : ModuleController
    {
        private readonly IGroupService _service;

        public GroupController(IGroupService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]GroupQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(GroupAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return await _service.Delete(id);
        }

        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Common]
        public async Task<IResultModel> Select()
        {
            return await _service.Select();
        }
    }
}
