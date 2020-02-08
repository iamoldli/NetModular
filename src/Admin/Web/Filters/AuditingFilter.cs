using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Module.AspNetCore.Attributes;
using NetModular.Module.Admin.Infrastructure;

namespace NetModular.Module.Admin.Web.Filters
{
    /// <summary>
    /// 审计过滤器
    /// </summary>
    public class AuditingFilter : IAsyncActionFilter
    {
        private readonly AdminOptions _options;
        private readonly IAuditingHandler _handler;

        public AuditingFilter(AdminOptions options, IAuditingHandler handler)
        {
            _options = options;
            _handler = handler;
        }

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_options.AuditingEnabled || CheckDisabled(context))
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
