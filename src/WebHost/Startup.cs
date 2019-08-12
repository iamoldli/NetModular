using Microsoft.AspNetCore.Hosting;
using Tm.Lib.Host.Web;

namespace Tm.WebHost
{
    public class Startup : StartupAbstract
    {
        public Startup(IHostingEnvironment env) : base(env)
        {
        }
    }
}
