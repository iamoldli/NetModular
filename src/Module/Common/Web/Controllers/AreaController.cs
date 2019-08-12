using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Common.Application.AreaService;
using Tm.Module.Common.Application.AreaService.ViewModels;
using Tm.Module.Common.Domain.Area;
using Tm.Module.Common.Domain.Area.Models;

namespace Tm.Module.Common.Web.Controllers
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
        [Common]
        public Task<IResultModel<IList<AreaEntity>>> QueryChildren(string parentCode)
        {
            return _service.QueryChildren(parentCode);
        }
    }
}
