using Microsoft.AspNetCore.Hosting;
using Nm.Lib.WebHost.Core;

namespace Nm.Module.Blog.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }
    }
}
