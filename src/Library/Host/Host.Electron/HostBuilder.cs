using Tm.Lib.Host.Web;
using System;
using System.Linq;
using ElectronNET.API;
using Microsoft.AspNetCore.Hosting;
using Tm.Lib.Logging.Serilog;

namespace Tm.Lib.Host.Electron
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
                Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                    .UseStartup<TStartup>()
                    .UseLogging()
                    .UseElectron(args)
                    .Build()
                    .Run();
            }
            else
            {
                new HostBuilder().Run<TStartup>(args);
            }
        }
    }
}
