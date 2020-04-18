using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Module.Admin.Infrastructure;

namespace NetModular.Module.Admin.Web.Filters
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {
        private readonly IAuditingHandler _handler;
        private readonly IConfigProvider _configProvider;

        public AuditingFilter(IAuditingHandler handler, IConfigProvider configProvider)
        {
            _handler = handler;
            _configProvider = configProvider;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var config = _configProvider.Get<AdminConfig>();
            if (!config.Auditing || CheckDisabled(context))
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
