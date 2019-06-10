using Microsoft.Extensions.Hosting;
using Nm.Lib.Utils.Core.Helpers;
using Serilog;

namespace Nm.Lib.Logging.Serilog.GenericHost
{
    public static class WebHostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var cfgHelper = new ConfigurationHelper();
                var cfg = cfgHelper.Load("logging", hostingContext.HostingEnvironment.EnvironmentName);

                loggerConfiguration
                    .ReadFrom.Configuration(cfg)
                    .Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
