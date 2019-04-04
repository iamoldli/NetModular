using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NetModular.Lib.WebHost.Core;

namespace NetModular.Module.Admin.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IConfiguration cfg, IHostingEnvironment env) : base(cfg, env)
        {
        }
    }
}
