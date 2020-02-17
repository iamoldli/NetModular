using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.AspNetCore
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
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 服务配置器
        /// </summary>
        public IModuleServicesConfigurator ServicesConfigurator { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }

        /// <summary>
        /// 数据库初始化数据脚本路径信息
        /// </summary>
        public ModuleInitDataScriptDescriptor InitDataScriptDescriptor { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public IModuleInitializer Initializer { get; set; }
    }
}
