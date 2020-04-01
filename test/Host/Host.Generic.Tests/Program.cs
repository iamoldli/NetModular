using System;
using System.Threading.Tasks;
using NetModular.Lib.Host.Generic;

namespace Host.Generic.Tests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new HostBuilder().Run<Startup>(args);
        }
    }
}
