using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.CodeGenerator.Application.ProjectService;
using Nm.Module.CodeGenerator.Application.ProjectService.ViewModels;
using Nm.Module.CodeGenerator.Domain.Project.Models;
using Nm.Module.CodeGenerator.Infrastructure.Options;

namespace Nm.Module.CodeGenerator.Web.Controllers
{
    [Description("项目管理")]
    public class ProjectController : ModuleController
    {
        private readonly IProjectService _service;
        private readonly ModuleCommonOptions _commonOptions;
        private readonly CodeGeneratorOptions _codeGeneratorOptions;
        public ProjectController(IProjectService service, IOptionsMonitor<ModuleCommonOptions> commonOptions, IOptionsMonitor<CodeGeneratorOptions> codeGeneratorOptions)
        {
            _service = service;
            _commonOptions = commonOptions.CurrentValue;
            _codeGeneratorOptions = codeGeneratorOptions.CurrentValue;
        }

        [HttpGet]
        [Description("查询")]
        public async Task<IResultModel> Query([FromQuery]ProjectQueryModel model)
        {
            return await _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ProjectAddModel model)
        {
            return _service.Add(model);
        }

        [HttpDelete]
        [Description("删除")]
        public Task<IResultModel> Delete([BindRequired]Guid id)
        {
            return _service.Delete(id);
        }

        [HttpGet]
        [Description("编辑")]
        public Task<IResultModel> Edit([BindRequired]Guid id)
        {
            return _service.Edit(id);
        }

        [HttpPost]
        [Description("修改")]
        public Task<IResultModel> Update(ProjectUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Description("生成代码")]
        public async Task<IActionResult> BuildCode(ProjectBuildCodeModel model)
        {
            var result = await _service.BuildCode(model);
            var path = Path.Combine(_commonOptions.TempPath, _codeGeneratorOptions.BuildCodePath, result.Data.Id + ".zip");
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", result.Data.Name + ".zip");
        }
    }
}
