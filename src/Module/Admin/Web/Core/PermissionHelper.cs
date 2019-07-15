using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Nm.Lib.Auth.Web.Attributes;
using Nm.Lib.Utils.Core.Attributes;
using Nm.Lib.Utils.Core.Enums;
using Nm.Lib.Utils.Mvc.Helpers;
using Nm.Module.Admin.Domain.Permission;

namespace Nm.Module.Admin.Web.Core
{
    [Singleton]
    public class PermissionHelper
    {
        private readonly MvcHelper _mvcHelper;

        public PermissionHelper(MvcHelper mvcHelper)
        {
            _mvcHelper = mvcHelper;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public List<PermissionEntity> GetAllPermission()
        {
            var list = new List<PermissionEntity>();
            var actions = _mvcHelper.GetAllAction();

            foreach (var action in actions)
            {
                //排除匿名接口和通用接口
                if (action.MethodInfo.CustomAttributes.Any(m => m.AttributeType == typeof(AllowAnonymousAttribute) || m.AttributeType == typeof(CommonAttribute)))
                    continue;

                var p = new PermissionEntity
                {
                    ModuleCode = action.Controller.Area,
                    Controller = action.Controller.Name,
                    Action = action.Name,
                    Name = action.Controller.Description ?? action.Controller.Name
                };

                var httpMethodAttr =
                    action.MethodInfo.CustomAttributes.FirstOrDefault(m => m.AttributeType.Name.StartsWith("Http"));

                if (httpMethodAttr != null)
                {
                    var httpMethodName = httpMethodAttr.AttributeType.Name.Replace("Http", "").Replace("Attribute", "").ToUpper();

                    p.HttpMethod = (HttpMethod)Enum.Parse(typeof(HttpMethod), httpMethodName);
                    p.Name += "_" + (action.Description ?? action.Name);
                    list.Add(p);
                }
            }

            return list;
        }
    }
}
