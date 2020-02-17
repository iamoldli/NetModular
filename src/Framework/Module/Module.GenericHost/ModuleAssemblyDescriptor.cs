using System.Reflection;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.GenericHost
{
    public class ModuleAssemblyDescriptor : IModuleAssemblyDescriptor
    {
        public Assembly Application { get; set; }

        public Assembly Domain { get; set; }

        public Assembly Infrastructure { get; set; }
        public Assembly Quartz { get; set; }
    }
}
