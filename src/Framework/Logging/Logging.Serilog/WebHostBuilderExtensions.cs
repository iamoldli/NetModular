using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace NetModular.Lib.Logging.Serilog
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder builder)
        {
            builder.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
            });

            return builder;
        }
    }
}
