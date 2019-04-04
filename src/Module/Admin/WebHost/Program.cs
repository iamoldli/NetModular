using NetModular.Lib.WebHost.Core;

namespace NetModular.Module.Admin.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHostCreator.Run<Startup>(args);
        }
    }
}
