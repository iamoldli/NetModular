using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Quartz.Application.JobClassService;
using Nm.Module.Quartz.Application.JobClassService.ViewModels;
using Nm.Module.Quartz.Domain.JobClass.Models;

namespace Nm.Module.Quartz.Web.Controllers
{
    [Description("任务类型管理")]
    public class JobClassController : ModuleController
    {
        private readonly IJobClassService _service;

        public JobClassController(IJobClassService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]JobClassQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(JobClassAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(JobClassUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
