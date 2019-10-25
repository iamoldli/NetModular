using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.GenericHost
{
    public class ModuleDescriptor : IModuleDescriptor
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }
    }
}
