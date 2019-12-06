#if NETCOREAPP2_2
using Microsoft.AspNetCore.Hosting;
#elif NETCOREAPP3_1
using Microsoft.Extensions.Hosting;
#endif
using NetModular.Lib.Host.Web;

namespace NetModular.Module.Admin.WebHost
{
    public class Startup : StartupAbstract
    {
#if NETCOREAPP2_2
        public Startup(IHostingEnvironment env) : base(env)
#elif NETCOREAPP3_1
        public Startup(IHostEnvironment env) : base(env)
#endif
        {
        }
    }
}
