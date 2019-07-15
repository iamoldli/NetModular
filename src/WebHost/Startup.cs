using Microsoft.AspNetCore.Hosting;
using Nm.Lib.Host.Web;

namespace Nm.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }
    }
}
