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
        #region ==属性==

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
        /// 提示说明
        /// </summary>
        public string Tooltip { get; set; }

        #endregion

        #region ==构造函数==

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
        /// <param name="tooltip">提示说明</param>
        public ModuleOptionDefinitionAttribute(string name, string tooltip)
        {
            Name = name;
            Tooltip = tooltip;
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

        /// <summary>
        /// 模块配置项定义
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="propertyType">指定属性类型</param>
        /// <param name="tooltip">提示说明</param>
        public ModuleOptionDefinitionAttribute(string name, ModuleOptionPropertyType propertyType, string tooltip)
        {
            Name = name;
            PropertyType = propertyType;
            Tooltip = tooltip;
        }

        #endregion
    }
}
