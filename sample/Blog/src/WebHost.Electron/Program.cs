using Nm.Lib.WebHost.Electron;

namespace Nm.Module.Blog.WebHost.Electron
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ElectronWebHostCreator.Run<Startup>(args);
        }
    }
}
