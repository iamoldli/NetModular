using Microsoft.AspNetCore.Builder;
#if NETSTANDARD2_0
using Microsoft.AspNetCore.Hosting;
#endif
using Microsoft.Extensions.DependencyInjection;
#if NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Utils.Core.Helpers;
using HostOptions = NetModular.Lib.Host.Web.Options.HostOptions;

namespace NetModular.Lib.Host.Web
{
    public abstract class StartupAbstract
    {
        protected readonly HostOptions HostOptions;
#if NETSTANDARD2_0
        protected readonly IHostingEnvironment Env;

        protected StartupAbstract(IHostingEnvironment env)
#elif NETCOREAPP3_1
        protected readonly IHostEnvironment Env;

        protected StartupAbstract(IHostEnvironment env)
#endif

        {
            Env = env;
            var cfgHelper = new ConfigurationHelper();
            //加载主机配置项
            HostOptions = cfgHelper.Get<HostOptions>("Host", env.EnvironmentName) ?? new HostOptions();
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(HostOptions, Env);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(HostOptions, Env);

            app.UseShutdownHandler();
        }
    }
}
