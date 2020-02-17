using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Module.Abstractions;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.AuditInfoService;
using NetModular.Module.Admin.Domain.AuditInfo;

namespace NetModular.Module.Admin.Web.Filters
{
    /// <summary>
    /// 审计日志处理
    /// </summary>
    public class AuditingHandler : IAuditingHandler
    {
        private readonly MvcHelper _mvcHelper;
        private readonly ILoginInfo _loginInfo;
        private readonly IAuditInfoService _auditInfoService;
        private readonly IModuleCollection _moduleCollection;

        public AuditingHandler(MvcHelper mvcHelper, ILoginInfo loginInfo, IAuditInfoService auditInfoService, IModuleCollection moduleCollection)
        {
            _mvcHelper = mvcHelper;
            _loginInfo = loginInfo;
            _auditInfoService = auditInfoService;
            _moduleCollection = moduleCollection;
        }

        public async Task Hand(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auditInfo = CreateAuditInfo(context);

            var sw = new Stopwatch();
            sw.Start();

            var resultContext = await next();

            sw.Stop();

            //执行结果
            auditInfo.Result = JsonSerializer.Serialize(resultContext.Result);
            //用时
            auditInfo.ExecutionDuration = sw.ElapsedMilliseconds;

            await _auditInfoService.Add(auditInfo);
        }

        private AuditInfoEntity CreateAuditInfo(ActionExecutingContext context)
        {
            var routeValues = context.ActionDescriptor.RouteValues;
            var auditInfo = new AuditInfoEntity
            {
                AccountId = _loginInfo.AccountId,
                AccountName = _loginInfo.AccountName,
                Area = routeValues["area"] ?? "",
                Controller = routeValues["controller"],
                Action = routeValues["action"],
                Parameters = JsonSerializer.Serialize(context.ActionArguments),
                Platform = _loginInfo.Platform,
                IP = _loginInfo.IP,
                ExecutionTime = DateTime.Now
            };

            //获取模块的名称
            if (auditInfo.Area.NotNull())
            {
                auditInfo.Module = _moduleCollection.FirstOrDefault(m => m.Id.EqualsIgnoreCase(auditInfo.Area))?.Name;
            }

            var controllerDescriptor = _mvcHelper.GetAllController().FirstOrDefault(m =>
                m.Area.EqualsIgnoreCase(auditInfo.Area) && m.Name.EqualsIgnoreCase(auditInfo.Controller));
            if (controllerDescriptor != null)
            {
                auditInfo.ControllerDesc = controllerDescriptor.Description;

                var actionDescription = _mvcHelper.GetAllAction().FirstOrDefault(m =>
                    m.Controller == controllerDescriptor && m.Name.EqualsIgnoreCase(auditInfo.Action));
                if (actionDescription != null)
                {
                    auditInfo.ActionDesc = actionDescription.Description;
                }
            }

            //记录浏览器UA
            if (_loginInfo.Platform == Platform.Web)
            {
                auditInfo.BrowserInfo = context.HttpContext.Request.Headers["User-Agent"];
            }

            return auditInfo;
        }

    }
}
