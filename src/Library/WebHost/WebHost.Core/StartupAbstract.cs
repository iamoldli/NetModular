using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetModular.Lib.WebHost.Core
{
    public abstract class StartupAbstract
    {
        private readonly IConfiguration _cfg;
        private readonly IHostingEnvironment _env;

        protected StartupAbstract(IConfiguration cfg, IHostingEnvironment env)
        {
            _cfg = cfg;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebHost(_cfg, _env);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebHost(_env);
        }
    }
}
