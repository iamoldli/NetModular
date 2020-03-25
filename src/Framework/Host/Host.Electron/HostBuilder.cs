using System;
using System.Linq;
using ElectronNET.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Host.Web;
using NetModular.Lib.Logging.Serilog;
using NetModular.Lib.Utils.Core.Helpers;
using HostOptions = NetModular.Lib.Host.Web.Options.HostOptions;

namespace NetModular.Lib.Host.Electron
{
    /// <summary>
    /// 主机生成器
    /// </summary>
    public class HostBuilder
    {
        public void Run<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            if (args != null && args.Any(arg => arg.Contains("/electron", StringComparison.OrdinalIgnoreCase)))
            {
                CreateBuilder<TStartup>(args).Build().Run();
            }
            else
            {
                new Web.HostBuilder().Run<TStartup>(args);
            }
        }

        /// <summary>
        /// 创建主机生成器
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public IHostBuilder CreateBuilder<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            var cfgHelper = new ConfigurationHelper();

            //加载主机配置项
            var hostOptions = cfgHelper.Get<HostOptions>("Host") ?? new HostOptions();

            if (hostOptions.Urls.IsNull())
                hostOptions.Urls = "http://*:5000";

            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseElectron(args)
                        .UseLogging()
                        .UseStartup<TStartup>()
                        .UseUrls(hostOptions.Urls);
                });
        }
    }
}
