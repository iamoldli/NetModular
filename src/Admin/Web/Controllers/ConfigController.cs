using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetModular.Lib.Auth.Web.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Module.Admin.Application.ConfigService;
using NetModular.Module.Admin.Application.ConfigService.ViewModels;
using NetModular.Module.Admin.Domain.Config;
using NetModular.Module.Admin.Domain.Config.Models;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("配置管理")]
    public class ConfigController : Web.ModuleController
    {
        private readonly IConfigService _service;

        public ConfigController(IConfigService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]ConfigQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(ConfigAddModel model)
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
        public Task<IResultModel> Update(ConfigUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("根据Key获取值")]
        [Common]
        public async Task<IResultModel> GetValue(string key, ConfigType type = ConfigType.System, string moduleCode = null)
        {
            if (key.IsNull())
                return ResultModel.Success(string.Empty);

            if (type == ConfigType.Module && moduleCode.IsNull())
                return ResultModel.Success(string.Empty);

            return await _service.GetValueByKey(key, type, moduleCode);
        }

        [HttpGet]
        [Common]
        public IResultModel TypeSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<ConfigType>());
        }
    }
}
