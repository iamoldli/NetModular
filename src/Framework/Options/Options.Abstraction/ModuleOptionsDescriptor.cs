using System;
using System.Collections.Generic;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 模块配置描述
    /// </summary>
    public class ModuleOptionsDescriptor
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        public IModuleDescriptor Module { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public Type OptionsType { get; set; }

        /// <summary>
        /// 配置项定义信息集合
        /// </summary>
        public List<ModuleOptionDefinitionAttribute> Definitions { get; } = new List<ModuleOptionDefinitionAttribute>();

        /// <summary>
        /// 变更事件类型
        /// </summary>
        public List<Type> ChangedEvents { get; } = new List<Type>();
    }
}
