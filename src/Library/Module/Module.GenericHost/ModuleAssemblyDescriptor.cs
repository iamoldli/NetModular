using System.Reflection;
using Tm.Lib.Module.Abstractions;

namespace Tm.Lib.Module.GenericHost
{
    public class ModuleAssemblyDescriptor : IModuleAssemblyDescriptor
    {
        public Assembly Application { get; set; }

        public Assembly Domain { get; set; }

        public Assembly Infrastructure { get; set; }
        public Assembly Quartz { get; set; }
    }
}
