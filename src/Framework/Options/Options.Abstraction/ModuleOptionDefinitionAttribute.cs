using System;
using System.Collections.Generic;
using System.Reflection;
using NetModular.Lib.Utils.Core.Result;
using Newtonsoft.Json;

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
        /// 唯一键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据名称
        /// </summary>
        public string DataName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ModuleOptionDataType DataType { get; set; }

        /// <summary>
        /// 枚举类型的选项列表
        /// </summary>
        public List<OptionResultModel> EnumOptions { get; set; }

        /// <summary>
        /// 提示说明
        /// </summary>
        public string Tooltip { get; set; }

        [JsonIgnore]
        public PropertyInfo PropertyInfo { get; set; }

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
        public ModuleOptionDefinitionAttribute(string name, ModuleOptionDataType propertyType)
        {
            Name = name;
            DataType = propertyType;
        }

        /// <summary>
        /// 模块配置项定义
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="propertyType">指定属性类型</param>
        /// <param name="tooltip">提示说明</param>
        public ModuleOptionDefinitionAttribute(string name, ModuleOptionDataType propertyType, string tooltip)
        {
            Name = name;
            DataType = propertyType;
            Tooltip = tooltip;
        }

        #endregion
    }
}
