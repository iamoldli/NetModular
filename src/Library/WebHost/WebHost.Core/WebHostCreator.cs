using Microsoft.AspNetCore.Hosting;
using NetModular.Lib.Logging.Serilog;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.WebHost.Core.Options;

namespace NetModular.Lib.WebHost.Core
{
    /// <summary>
    /// WebHost构造器
    /// </summary>
    public class WebHostCreator
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args">启动参数</param>
        public static void Run<TStartup>(string[] args) where TStartup : StartupAbstract
        {
            //加载主机配置项
            var hostOptions = ConfigurationExtensions.Get<HostOptions>("Host");

            if (hostOptions.Urls.IsNull())
                hostOptions.Urls = "http://*:5000";

            Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup<TStartup>()
                .UseLogging()
                .UseUrls(hostOptions.Urls)
                .Build()
                .Run();
        }
    }
}
