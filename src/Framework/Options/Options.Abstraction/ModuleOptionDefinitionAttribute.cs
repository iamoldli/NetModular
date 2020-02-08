using System;
using System.Collections.Generic;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 配置定义特性，用于描述配置信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ModuleOptionDefinitionAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public ModuleOptionPropertyType PropertyType { get; set; }

        /// <summary>
        /// 枚举类型的选项列表
        /// </summary>
        public List<OptionResultModel> EnumOptions { get; set; }

        /// <summary>
        /// 模块配置项定义
        /// </summary>
        /// <param name="name">名称</param>
        public ModuleOptionDefinitionAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 模块配置项定义
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="propertyType">指定属性类型</param>
        public ModuleOptionDefinitionAttribute(string name, ModuleOptionPropertyType propertyType)
        {
            Name = name;
            PropertyType = propertyType;
        }
    }
}
