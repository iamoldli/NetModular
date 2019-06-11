using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.MySql;
using Nm.Lib.Logging.Serilog.GenericHost;
using Nm.Lib.Utils.Core.Helpers;
using Nm.Module.Common.Application.AreaService;
using Nm.Module.Common.Domain.Area;
using Nm.Module.Common.Infrastructure.AreaCrawling;
using Nm.Module.Common.Infrastructure.Repositories;
using Nm.Module.Common.Infrastructure.Repositories.MySql;

namespace Nm.Module.Common.AreaCrawling
{
    class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var cfgHelper = new ConfigurationHelper();
                    var dbOptions = cfgHelper.Get<DbOptions>("Db", hostContext.HostingEnvironment.EnvironmentName);

                    //日志工厂
                    var loggerFactory = dbOptions.Logging ? services.BuildServiceProvider().GetService<ILoggerFactory>() : null;

                    dbOptions.Connections[0].EntityTypes = new List<Type>
                    {
                        typeof(AreaEntity)
                    };

                    var dbContextOptions = new MySqlDbContextOptions(dbOptions, dbOptions.Connections[0], loggerFactory, null);
                    services.AddSingleton<IDbContext>(new CommonDbContext(dbContextOptions));
                    services.AddSingleton<IUnitOfWork<CommonDbContext>, UnitOfWork<CommonDbContext>>();
                    services.AddSingleton<IAreaRepository, AreaRepository>();
                    services.AddSingleton<IAreaService, AreaService>();

                    services.AddSingleton<IAreaCrawlingHandler, AreaCrawlingHandler>();

                    var config = new MapperConfiguration(cfg =>
                    {
                        new MapperConfig().Bind(cfg);
                    });

                    services.AddSingleton(config.CreateMapper());

                    services.AddHostedService<Startup>();

                    Console.WriteLine("注入服务完成");
                })
                .UseLogging()
                .Build();

            Console.WriteLine("准备启动");

            host.Start();

            host.WaitForShutdown();
        }
    }
}
