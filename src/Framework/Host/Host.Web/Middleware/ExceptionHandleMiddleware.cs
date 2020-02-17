using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Host.Web.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILogger _logger;
        public ExceptionHandleMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionHandleMiddleware> logger)
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
