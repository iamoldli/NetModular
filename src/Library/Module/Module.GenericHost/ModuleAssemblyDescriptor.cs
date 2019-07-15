using System.Reflection;
using Nm.Lib.Module.Abstractions;

namespace Nm.Lib.Module.GenericHost
{
    public class ModuleAssemblyDescriptor : IModuleAssemblyDescriptor
    {
        public Assembly Application { get; set; }

        public Assembly Domain { get; set; }

        public Assembly Infrastructure { get; set; }
    }
}
