using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Nm.Lib.Utils.Core.Helpers;
using Nm.Lib.WebHost.Core.Options;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Nm.Lib.WebHost.Core
{
    public abstract class StartupAbstract
    {
        private readonly HostOptions _hostOptions;
        private readonly IHostingEnvironment _env;

        protected StartupAbstract(IHostingEnvironment env)
        {
            _env = env;
            var cfgHelper = new ConfigurationHelper();
            //加载主机配置项
            _hostOptions = cfgHelper.Get<HostOptions>("Host", env.EnvironmentName);
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(_hostOptions, _env);
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(_hostOptions, _env);
        }
    }
}
