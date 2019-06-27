using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.MediaTypeService;
using Nm.Module.Common.Application.MediaTypeService.ViewModels;
using Nm.Module.Common.Domain.MediaType.Models;

namespace Nm.Module.Common.Web.Controllers
{
    [Description("多媒体管理")]
    public class MediaTypeController : ModuleController
    {
        private readonly IMediaTypeService _service;

        public MediaTypeController(IMediaTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]MediaTypeQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MediaTypeAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public async Task<IResultModel> Delete([BindRequired]int id)
        {
            return await _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public async Task<IResultModel> Edit([BindRequired]int id)
        {
            return await _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(MediaTypeUpdateModel model)
        {
            return _service.Update(model);
        }

    }
}
