using System.Collections.Generic;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Module.GenericHost
{
    public class ModuleDescriptor : IModuleDescriptor
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

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
        /// 说明介绍
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 服务配置器
        /// </summary>
        public IModuleServicesConfigurator ServicesConfigurator { get; set; }

        /// <summary>
        /// 程序集
        /// </summary>
        public IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }

        /// <summary>
        /// 数据库初始化脚本路径信息
        /// </summary>
        public ModuleInitDataScriptDescriptor InitDataScriptDescriptor { get; set; }

        /// <summary>
        /// 枚举信息
        /// </summary>
        public List<ModuleEnumDescriptor> EnumDescriptors { get; set; } = new List<ModuleEnumDescriptor>();

    }
}
