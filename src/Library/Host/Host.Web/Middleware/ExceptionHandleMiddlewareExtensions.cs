

using Microsoft.AspNetCore.Builder;

namespace Nm.Lib.Host.Web.Middleware
{
    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandle(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandleMiddleware>();

            return app;
        }
    }
}
