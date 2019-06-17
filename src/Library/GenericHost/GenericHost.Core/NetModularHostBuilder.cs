using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Nm.Lib.Logging.Serilog.GenericHost;

namespace Nm.Lib.GenericHost.Core
{
    public class NetModularHostBuilder
    {
        public IHost Build(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.AddEnvironmentVariables();
                    configHost.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {

                })
                .UseLogging()
                .Build();

            return host;
        }
    }
}
