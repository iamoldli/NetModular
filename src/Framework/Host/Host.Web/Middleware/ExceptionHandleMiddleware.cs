using System;
using System.Net;
using System.Threading.Tasks;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.AspNetCore.Http;
#if NETCOREAPP3_0
using Microsoft.Extensions.Hosting;
#endif
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Host.Web.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
#if NETSTANDARD2_0
        private readonly IHostingEnvironment _env;
#elif NETCOREAPP3_0
        private readonly IHostEnvironment _env;
#endif
        private readonly ILogger _logger;
#if NETSTANDARD2_0
        public ExceptionHandleMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<ExceptionHandleMiddleware> logger)
#elif NETCOREAPP3_0
        public ExceptionHandleMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionHandleMiddleware> logger)
#endif
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = _env.IsDevelopment() ? exception.ToString() : exception.Message;

            _logger.LogError(error);

            return context.Response.WriteAsync(JsonConvert.SerializeObject(ResultModel.Failed(error), new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
