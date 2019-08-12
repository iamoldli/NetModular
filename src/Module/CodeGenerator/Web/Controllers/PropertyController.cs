using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Tm.Lib.Auth.Web.Attributes;
using Tm.Lib.Utils.Core.Extensions;
using Tm.Lib.Utils.Core.Models;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.CodeGenerator.Application.PropertyService;
using Tm.Module.CodeGenerator.Application.PropertyService.ViewModels;
using Tm.Module.CodeGenerator.Domain.Property;
using Tm.Module.CodeGenerator.Domain.Property.Models;

namespace Tm.Module.CodeGenerator.Web.Controllers
{
    [Description("实体属性管理")]
    [Common]
    public class PropertyController : ModuleController
    {
        private readonly IPropertyService _service;

        public PropertyController(IPropertyService service)
        {
            _service = service;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]PropertyQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("添加")]
        public Task<IResultModel> Add(PropertyAddModel model)
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
        public Task<IResultModel> Update(PropertyUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("获取属性类型下拉列表")]
        public IResultModel PropertyTypeSelect()
        {
            return ResultModel.Success(EnumExtensions.ToResult<PropertyType>(true));
        }

        [HttpGet]
        [Description("获取排序信息")]
        public Task<IResultModel> Sort([BindRequired]Guid classId)
        {
            return _service.QuerySortList(classId);
        }

        [HttpPost]
        [Description("更新排序信息")]
        public Task<IResultModel> Sort(SortUpdateModel<Guid> model)
        {
            return _service.UpdateSortList(model);
        }

        [HttpPost]
        [Description("修改可空状态")]
        public Task<IResultModel> UpdateNullable(PropertyUpdateNullableModel model)
        {
            return _service.UpdateNullable(model);
        }

        [HttpPost]
        [Description("修改列表显示状态")]
        public Task<IResultModel> UpdateShowInList(PropertyUpdateShowInListModel model)
        {
            return _service.UpdateShowInList(model);
        }

        [HttpGet]
        [Description("获取下拉列表")]
        public Task<IResultModel> Select([BindRequired]Guid classId)
        {
            return _service.Select(classId);
        }
    }
}
