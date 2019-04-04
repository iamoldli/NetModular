using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using NetModular.Lib.Auth.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;
using NetModular.Module.Admin.Application.PermissionService;
using NetModular.Module.Admin.Application.PermissionService.ViewModels;

namespace NetModular.Module.Admin.Web.Controllers
{
    [Description("权限接口")]
    public class PermissionController : ModuleController
    {
        private readonly IPermissionService _service;
        private readonly ApplicationPartManager _partManager;
        private readonly IMemoryCache _cache;

        public PermissionController(IPermissionService service, ApplicationPartManager partManager, IMemoryCache cache)
        {
            _service = service;
            _partManager = partManager;
            _cache = cache;
        }

        [HttpGet]
        [Description("查询")]
        public Task<IResultModel> Query([FromQuery]PermissionQueryModel model)
        {
            return _service.Query(model);
        }

        [HttpPost]
        [Description("新增")]
        public async Task<IResultModel> Add(PermissionAddModel model)
        {
            if (!model.Actions.Any())
                return ResultModel.Failed("请选择操作");
            return await _service.Add(model);
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
        [Description("更新")]
        public Task<IResultModel> Update(PermissionUpdateModel model)
        {
            return _service.Update(model);
        }

        [HttpGet]
        [Description("获取控制器下拉列表")]
        public IResultModel Controllers([BindRequired]string moduleCode)
        {
            var cacheKey = "ADMIN_GET_CONTROLLERS_" + moduleCode.ToUpper();
            if (!_cache.TryGetValue(cacheKey, out List<OptionResultModel> list))
            {
                list = new List<OptionResultModel>();
                var controllerFeature = new ControllerFeature();
                _partManager.PopulateFeature(controllerFeature);
                var controllers = controllerFeature.Controllers.ToList();
                foreach (var controller in controllers)
                {
                    if (!controller.IsAbstract && controller.FullName != null && controller.FullName.ToLower().Contains($".{moduleCode}."))
                    {
                        var option = new OptionResultModel
                        {
                            Value = controller.Name.Remove(controller.Name.Length - 10),
                            Data = controller.Name
                        };

                        var desc = (DescriptionAttribute)Attribute.GetCustomAttribute(controller, typeof(DescriptionAttribute));
                        if (desc == null || desc.Description.IsNull())
                        {
                            option.Label = option.Value.ToString();
                        }
                        else
                        {
                            option.Label = desc.Description;
                        }

                        list.Add(option);
                    }
                }

                _cache.Set(cacheKey, list);
            }

            return ResultModel.Success(list);
        }

        [HttpGet]
        [Description("获取控制器方法下拉列表")]
        public IResultModel Actions([BindRequired]string moduleCode, [BindRequired]string controller)
        {
            var cacheKey = $"ADMIN_GET_ACTIONS_{moduleCode.ToUpper()}_{controller.ToLower()}";
            if (!_cache.TryGetValue(cacheKey, out List<OptionResultModel> list))
            {
                list = new List<OptionResultModel>();
                var controllerFeature = new ControllerFeature();
                _partManager.PopulateFeature(controllerFeature);
                var controllers = controllerFeature.Controllers.ToList();
                foreach (var con in controllers)
                {
                    if (con.FullName != null && con.FullName.ToLower().Contains($".{moduleCode}.") && con.Name.Remove(con.Name.Length - 10).EqualsIgnoreCase(controller))
                    {
                        foreach (var method in con.DeclaredMethods)
                        {
                            //排除可匿名访问以及通用方法
                            if (method.CustomAttributes.Any(m => m.AttributeType == typeof(AllowAnonymousAttribute) || m.AttributeType == typeof(CommonAttribute)))
                            {
                                continue;
                            }

                            var option = new OptionResultModel
                            {
                                Value = method.Name
                            };

                            var descAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(method, typeof(DescriptionAttribute));
                            if (descAttr == null || descAttr.Description.IsNull())
                            {
                                option.Label = option.Value.ToString();
                            }
                            else
                            {
                                option.Label = descAttr.Description;
                            }

                            list.Add(option);
                        }

                        break;
                    }
                }

                _cache.Set(cacheKey, list);
            }

            //排除已添加过得权限
            var permissions = _service.QueryControllerActions(moduleCode, controller).Result;
            list.ForEach(m =>
            {
                foreach (var permission in permissions)
                {
                    if (permission.Action.EqualsIgnoreCase(m.Value.ToString()))
                    {
                        m.Disabled = true;
                        break;
                    }
                }
            });

            return ResultModel.Success(list);
        }
    }
}
