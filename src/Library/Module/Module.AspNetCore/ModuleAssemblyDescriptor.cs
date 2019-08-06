using System.Reflection;
using Nm.Lib.Module.Abstractions;

namespace Nm.Lib.Module.AspNetCore
{
    public class ModuleAssemblyDescriptor : IModuleAssemblyDescriptor
    {
        public Assembly Web { get; set; }

        public Assembly Application { get; set; }

        public Assembly Domain { get; set; }

        public Assembly Infrastructure { get; set; }

        public Assembly Quartz { get; set; }
    }
}
