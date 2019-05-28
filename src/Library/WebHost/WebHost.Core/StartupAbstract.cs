using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Utils.Core.Helpers;
using Nm.Lib.WebHost.Core.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Nm.Lib.WebHost.Core
{
    public abstract class StartupAbstract
    {
        protected readonly HostOptions HostOptions;
        protected readonly IHostingEnvironment Env;

        protected StartupAbstract(IHostingEnvironment env)
        {
            Env = env;
            var cfgHelper = new ConfigurationHelper();
            //加载主机配置项
            HostOptions = cfgHelper.Get<HostOptions>("Host", env.EnvironmentName);
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(HostOptions, Env);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(HostOptions, Env);
        }
    }
}
