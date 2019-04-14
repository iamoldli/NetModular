using Microsoft.AspNetCore.Builder;

// ReSharper disable once IdentifierTypo
namespace NetModular.Lib.WebHost.Core.Middlewares
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
