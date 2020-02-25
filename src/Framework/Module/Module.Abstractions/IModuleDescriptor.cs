using System.Collections.Generic;

namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块描述
    /// </summary>
    public interface IModuleDescriptor
    {
        /// <summary>
        /// 编号
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        string Icon { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 服务配置器
        /// </summary>
        IModuleServicesConfigurator ServicesConfigurator { get; set; }

        /// <summary>
        /// 程序集信息
        /// </summary>
        IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }

        /// <summary>
        /// 初始化数据库脚本路径信息
        /// </summary>
        ModuleInitDataScriptDescriptor InitDataScriptDescriptor { get; set; }

        /// <summary>
        /// 枚举描述器列表
        /// </summary>
        List<ModuleEnumDescriptor> EnumDescriptors { get; set; }
    }
}
