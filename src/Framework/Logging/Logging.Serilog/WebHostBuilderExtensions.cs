using Microsoft.AspNetCore.Hosting;
using NetModular.Lib.Utils.Core.Helpers;
using Serilog;

namespace NetModular.Lib.Logging.Serilog
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                var cfgHelper = new ConfigurationHelper();
                var cfg = cfgHelper.Load("logging", hostingContext.HostingEnvironment.EnvironmentName);
                if (cfg != null)
                {
                    loggerConfiguration.ReadFrom.Configuration(cfg);
                }

                loggerConfiguration.Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
