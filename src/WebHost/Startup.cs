using Microsoft.AspNetCore.Hosting;
using Nm.Lib.WebHost.Core;

namespace NetModular.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }
    }
}
