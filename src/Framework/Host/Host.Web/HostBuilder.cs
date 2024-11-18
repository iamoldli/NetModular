using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Logging.Serilog;
using HostOptions = NetModular.Lib.Host.Web.Options.HostOptions;

namespace NetModular.Lib.Host.Web
{
    /// <summary>
    /// WebHost构造器
    /// </summary>
    public class HostBuilder
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="args">启动参数</param>
        /// <param name="configureService"></param>
        /// <param name="configure"></param>
        public async Task RunAsync(string[] args, Action<IServiceCollection> configureService = null, Action<WebApplication> configure = null)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", false);

            var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentVariable.NotNull())
            {
                configBuilder.AddJsonFile($"appsettings.{environmentVariable}.json", false);
            }

            var config = configBuilder.Build();

            var hostOptions = new HostOptions();
            config.GetSection("Host").Bind(hostOptions);

            if (hostOptions.Urls.IsNull())
                hostOptions.Urls = "http://*:5000";

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseLogging();
            builder.Services.AddSingleton(hostOptions);
            builder.Services.AddWebHost(hostOptions, builder.Environment, builder.Configuration);

            configureService?.Invoke(builder.Services);

            var app = builder.Build();

            app.UseWebHost(hostOptions, builder.Environment);

            app.UseShutdownHandler();

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                //显示启动Logo
                app.Services.GetService<IStartLogoProvider>().Show(hostOptions);
            });

            configure?.Invoke(app);

            await app.RunAsync();
        }
    }
}
