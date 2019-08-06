using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Quartz.Application.JobService;
using Nm.Module.Quartz.Application.JobService.ViewModels;
using Nm.Module.Quartz.Domain.Job.Models;
using Nm.Module.Quartz.Domain.JobLog.Models;
using Nm.Module.Quartz.Web.Core;

namespace Nm.Module.Quartz.Web.Controllers
{
    [Description("任务管理")]
    public class JobController : ModuleController
    {
        private readonly IJobService _service;
        private readonly JobHelper _helper;

        public JobController(IJobService service, JobHelper helper)
        {
            _service = service;
            _helper = helper;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]JobQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(JobAddModel model)
        {
            return _service.Add(model);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(JobUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }

        [HttpPost]
        [Description("暂停")]
        public  Task<IResultModel> Pause([BindRequired]Guid id)
        {
            return  _service.Pause(id);
        }

        [HttpPost]
        [Description("启动")]
        public Task<IResultModel> Resume([BindRequired]Guid id)
        {
            return _service.Resume(id);
        }

        [HttpGet]
        [Description("日志")]
        public Task<IResultModel> Log([FromQuery]JobLogQueryModel model)
        {
            return _service.Log(model);
        }

        [HttpGet]
        [Common]
        public IResultModel ModuleSelect()
        {
            return ResultModel.Success(_helper.ModuleSelect);
        }

        [HttpGet]
        [Common]
        public IResultModel JobSelect(string moduleId)
        {
            return ResultModel.Success(_helper.GetJobSelect(moduleId));
        }
    }
}
