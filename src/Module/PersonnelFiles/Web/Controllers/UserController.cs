using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Nm.Lib.Utils.Core.Options;
using Nm.Lib.Utils.Core.Result;
using Nm.Lib.Utils.Mvc.Extensions;
using Nm.Lib.Utils.Mvc.Helpers;
using Nm.Module.PersonnelFiles.Application.UserService;
using Nm.Module.PersonnelFiles.Application.UserService.ViewModels;
using Nm.Module.PersonnelFiles.Domain.User.Models;

namespace Nm.Module.PersonnelFiles.Web.Controllers
{
    [Description("用户信息管理")]
    public class UserController : ModuleController
    {
        private readonly ModuleCommonOptions _options;
        private readonly FileUploadHelper _fileUploadHelper;
        private readonly IUserService _service;

        public UserController(IUserService service, IOptionsMonitor<ModuleCommonOptions> optionsMonitor, FileUploadHelper fileUploadHelper)
        {
            _service = service;
            _options = optionsMonitor.CurrentValue;
            _fileUploadHelper = fileUploadHelper;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery] UserQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(UserAddModel model)
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
        public Task<IResultModel> Update(UserUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpPost]
        [Description("上传照片")]
        public async Task<IResultModel> UploadPicture(IFormFile formFile)
        {
            var model = new FileUploadModel
            {
                Request = Request,
                FormFile = formFile,
                RootPath = _options.UploadPath,
                Module = "PersonnelFiles",
                Group = "UserPicture"
            };

            var result = await _fileUploadHelper.Upload(model);

            if (result.Successful)
            {
                var file = result.Data;

                file.Url = new Uri(Request.GetHost($"/upload/{file.FullPath.ToLower()}")).ToString().ToLower();

                return ResultModel.Success(file);
            }

            return ResultModel.Failed("上传失败");
        }
    }
}
