using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Lib.Utils.Core.SystemConfig;
using NetModular.Module.Admin.Infrastructure.Options;

namespace NetModular.Module.Admin.Web.Filters
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {
        private readonly AdminOptions _options;
        private readonly IAuditingHandler _handler;
        private readonly SystemConfigModel _systemConfig;

        public AuditingFilter(IOptionsMonitor<AdminOptions> optionsAccessor, IAuditingHandler handler, SystemConfigModel systemConfig)
        {
            _options = optionsAccessor.CurrentValue;
            _handler = handler;
            _systemConfig = systemConfig;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_systemConfig.Permission.Auditing || !_options.Auditing || CheckDisabled(context))
            {
                return next();
            }

            return _handler.Hand(context, next);
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
