using System;
using System.Collections.Generic;

namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块枚举描述器
    /// </summary>
    public class ModuleEnumDescriptor
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 库名称
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// 枚举类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 列表
        /// </summary>
        public List<OptionResultModel> Options { get; set; }
    }
}
