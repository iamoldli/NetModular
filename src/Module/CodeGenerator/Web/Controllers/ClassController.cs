using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ClassService;
using Nm.Module.CodeGenerator.Application.ClassService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Class;
using Nm.Module.CodeGenerator.Domain.Class.Models;
using Nm.Module.CodeGenerator.Infrastructure.Options;

namespace Nm.Module.CodeGenerator.Web.Controllers
{
    [Description("实体管理")]
    [Common]
    public class ClassController : ModuleController
    {
        private readonly IClassService _service;
        private readonly ModuleCommonOptions _commonOptions;
        private readonly CodeGeneratorOptions _codeGeneratorOptions;

        public ClassController(IClassService service, IOptionsMonitor<ModuleCommonOptions> commonOption, IOptionsMonitor<CodeGeneratorOptions> codeGeneratorOptions)
        {
            _service = service;
            _codeGeneratorOptions = codeGeneratorOptions.CurrentValue;
            _commonOptions = commonOption.CurrentValue;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ClassQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ClassAddModel model)
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
        public Task<IResultModel> Update(ClassUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("获取基类类型下拉列表")]
        [Common]
        public IResultModel BaseEntityTypeSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<BaseEntityType>(true));
        }

        [HttpGet]
        [Description("生成代码")]
        public async Task<IActionResult> BuildCode([BindRequired]Guid id)
        {
            var result = await _service.BuildCode(id);
            var path = Path.Combine(_commonOptions.TempPath, _codeGeneratorOptions.BuildCodePath, result.Data.Id + ".zip");
            return PhysicalFile(path, "application/octet-stream", HttpUtility.UrlEncode(result.Data.Name), true);
        }
    }
}
