using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.PersonnelFiles.Application.CompanyService;
using Nm.Module.PersonnelFiles.Application.CompanyService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.Company.Models;

namespace Nm.Module.PersonnelFiles.Web.Controllers
{
    [Description("公司单位管理")]
    public class CompanyController : ModuleController
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] CompanyQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(CompanyAddModel model)
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
        public Task<IResultModel> Update(CompanyUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("下拉列表")]
        [Common]
        public async Task<IResultModel> Select()
        {
            return await _service.Select();
        }
    }
}
