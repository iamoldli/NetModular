using System;

namespace NetModular.Lib.Config.Abstraction
{
    /// <summary>
    /// 配置定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigDefinitionAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        public ConfigDefinitionAttribute(string name)
        {
            Name = name;
        }
    }
}
