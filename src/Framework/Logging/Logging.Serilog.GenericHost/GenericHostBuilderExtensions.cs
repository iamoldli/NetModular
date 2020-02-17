using Microsoft.Extensions.Hosting;
using NetModular.Lib.Utils.Core.Helpers;
using Serilog;

namespace NetModular.Lib.Logging.Serilog.GenericHost
{
    public static class WebHostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var cfgHelper = new ConfigurationHelper();
                var cfg = cfgHelper.Load("logging", hostingContext.HostingEnvironment.EnvironmentName);

                if (cfg != null)
                {
                    loggerConfiguration
                        .ReadFrom.Configuration(cfg);
                }

                loggerConfiguration.Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
