using Microsoft.AspNetCore.Hosting;
using NetModular.Lib.Utils.Core.Extensions;
using Serilog;

namespace NetModular.Lib.Logging.Serilog
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var cfg = ConfigurationExtensions.Load("logging",
                      hostingContext.HostingEnvironment.EnvironmentName);

                loggerConfiguration
                    .ReadFrom.Configuration(cfg)
                    .Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
