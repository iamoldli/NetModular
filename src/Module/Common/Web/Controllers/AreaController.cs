using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AreaService;
using Nm.Module.Common.Application.AreaService.ViewModels;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Domain.Area.Models;

namespace Nm.Module.Common.Web.Controllers
{
    [Description("区划代码管理")]
    public class AreaController : ModuleController
    {
        private readonly IAreaService _service;

        public AreaController(IAreaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] AreaQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(AreaAddModel model)
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
        public Task<IResultModel> Update(AreaUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("查询子节点")]
        public Task<IResultModel<IList<AreaEntity>>> QueryChildren(int? parentId)
        {
            return _service.QueryChildren(parentId ?? 0);
        }
    }
}
