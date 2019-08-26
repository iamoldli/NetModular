using Nm.Lib.Host.Web;

namespace Nm.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new HostBuilder().Run<Startup>(args);

        }
    }
}
