using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 审计日志处理接口
    /// </summary>
    public interface IAuditingHandler
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="context">请求上下文</param>
        /// <param name="next">请求委托</param>
        /// <returns></returns>
        Task Hand(ActionExecutingContext context, ActionExecutionDelegate next);
    }
}
