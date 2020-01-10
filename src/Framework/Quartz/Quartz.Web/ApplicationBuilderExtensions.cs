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
#if NETSTANDARD2_0
        public static IApplicationBuilder StartQuartz(this IApplicationBuilder app)
#elif NETCOREAPP3_1
        public static IApplicationBuilder StartQuartz(this IApplicationBuilder app)
#endif
        {
            //启动
            var quartzServer = app.ApplicationServices.GetService<IQuartzServer>();
            if (quartzServer != null)
            {
                quartzServer.Start().GetAwaiter().GetResult();
            }

            return app;
        }
    }
}
