using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetModular.Lib.Cache.Integration;
using NetModular.Lib.Data.Integration;
using NetModular.Lib.Logging.Serilog.GenericHost;
using NetModular.Lib.Mapper.AutoMapper;
using NetModular.Lib.Module.GenericHost;
using NetModular.Lib.Utils.Core;

namespace NetModular.Lib.Host.Generic
{
    /// <summary>
    /// 主机生成器
    /// </summary>
    public class HostBuilder
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="configureServices"></param>
        /// <returns></returns>
        public IHost Build<TStartup>(string[] args, Action<IServiceCollection, IHostEnvironment> configureServices = null) where TStartup : class, IHostedService
        {
            // 解决乱码问题
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .UseLogging()
                .UseDefaultServiceProvider(options => { options.ValidateOnBuild = false; })
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var envName = hostContext.HostingEnvironment.EnvironmentName;

                    services.AddUtils();

                    //加载模块
                    var modules = services.AddModules(envName);

                    //添加对象映射
                    services.AddMappers(modules);

                    //添加缓存
                    services.AddCache(envName);

                    //添加数据库
                    services.AddDb(envName, modules);

                    //自定义服务
                    configureServices?.Invoke(services, hostContext.HostingEnvironment);

                    //添加主机服务
                    services.AddHostedService<TStartup>();

                    //添加HttpClient相关
                    services.AddHttpClient();
                })
                .Build();

            return host;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="configureServices"></param>
        public async Task Run<TStartup>(string[] args, Action<IServiceCollection, IHostEnvironment> configureServices = null) where TStartup : class, IHostedService
        {
            var host = Build<TStartup>(args, configureServices);

            using (host)
            {
                await host.StartAsync();

                await host.WaitForShutdownAsync();
            }
        }
    }
}
