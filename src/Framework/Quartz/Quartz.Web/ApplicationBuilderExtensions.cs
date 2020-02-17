using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Quartz.Abstractions;

namespace NetModular.Lib.Quartz.Web
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用WebHost
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder StartQuartz(this IApplicationBuilder app)
        {
            //启动
            var quartzServer = app.ApplicationServices.GetService<IQuartzServer>();
            quartzServer?.Start().GetAwaiter().GetResult();
            return app;
        }
    }
}
