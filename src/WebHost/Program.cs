using Nm.Lib.WebHost.Core;

namespace Nm.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHostCreator.Run<Startup>(args);
        }
    }
}
