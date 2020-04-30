using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Module.Admin.Application.MimeService;
using NetModular.Module.Admin.Application.MimeService.ViewModels;
using NetModular.Module.Admin.Domain.Mime.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("MIME管理")]
    public class MimeController : Web.ModuleController
    {
        private readonly IMimeService _service;

        public MimeController(IMimeService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]MimeQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(MimeAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]int id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]int id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(MimeUpdateModel model)
        {
            return _service.Update(model);
        }
    }
}
