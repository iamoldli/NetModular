using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Mvc.Helpers;
using NetModular.Module.Admin.Application.AuditInfoService;
using NetModular.Module.Admin.Application.SystemService;
using NetModular.Module.Admin.Domain.AuditInfo;
using NetModular.Module.Admin.Infrastructure.Options;

namespace NetModular.Module.Admin.Web.Filters
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {
        private readonly AdminOptions _options;
        private readonly ILoginInfo _loginInfo;
        private readonly IAuditInfoService _auditInfoService;
        private readonly ISystemService _systemService;
        private readonly MvcHelper _mvcHelper;

        public AuditingFilter(IOptionsMonitor<AdminOptions> optionsAccessor, IAuditInfoService auditInfoService, ILoginInfo loginInfo, ISystemService systemService, MvcHelper mvcHelper)
        {
            _options = optionsAccessor.CurrentValue;
            _auditInfoService = auditInfoService;
            _loginInfo = loginInfo;
            _systemService = systemService;
            _mvcHelper = mvcHelper;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cfg = _systemService.GetConfig().Result;
            if (!cfg.Data.Auditing || !_options.Auditing || CheckDisabled(context))
            {
                return next();
            }

            return ExecuteAuditing(context, next);
        }

        /// <summary>
        /// 执行审计功能
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        private async Task ExecuteAuditing(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var auditInfo = CreateAuditInfo(context);

            var sw = new Stopwatch();
            sw.Start();

            var resultContext = await next();

            sw.Stop();

            //执行结果
            auditInfo.Result = JsonConvert.SerializeObject(resultContext.Result);
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
                Area = routeValues["area"] ?? "",
                Controller = routeValues["controller"],
                Action = routeValues["action"],
                Parameters = JsonConvert.SerializeObject(context.ActionArguments),
                Platform = _loginInfo.Platform,
                IP = _loginInfo.IP,
                ExecutionTime = DateTime.Now
            };

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

        /// <summary>
        /// 判断是否禁用审计功能
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool CheckDisabled(ActionExecutingContext context)
        {
            return context.ActionDescriptor.EndpointMetadata.Any(m => m.GetType() == typeof(DisableAuditingAttribute));
        }
    }
}
