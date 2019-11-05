using Microsoft.AspNetCore.Hosting;
#if NETCOREAPP3_0
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Logging.Serilog;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;
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
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args">启动参数</param>
        public void Run<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            CreateBuilder<TStartup>(args).Build().Run();
        }

        /// <summary>
        /// 创建主机生成器
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
#if NETSTANDARD2_0
        public IWebHostBuilder CreateBuilder<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            var cfgHelper = new ConfigurationHelper();

            //加载主机配置项
            var hostOptions = cfgHelper.Get<HostOptions>("Host") ?? new HostOptions();

            if (hostOptions.Urls.IsNull())
                hostOptions.Urls = "http://*:5000";

            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseLogging()
                .UseStartup<TStartup>()
                .UseUrls(hostOptions.Urls);
        }
#elif NETCOREAPP3_0
        public IHostBuilder CreateBuilder<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            var cfgHelper = new ConfigurationHelper();

            //加载主机配置项
            var hostOptions = cfgHelper.Get<HostOptions>("Host") ?? new HostOptions();

            if (hostOptions.Urls.IsNull())
                hostOptions.Urls = "http://*:5000";

            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                    .UseDefaultServiceProvider(options => { options.ValidateOnBuild = false; })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseLogging()
                            .UseStartup<TStartup>()
                            .UseUrls(hostOptions.Urls);
                    });
        }
#endif
    }
}
