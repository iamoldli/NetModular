using System.ComponentModel;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置类型
    /// </summary>
    public enum ConfigType
    {
        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义配置")]
        Custom,
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统配置")]
        System,
        /// <summary>
        /// 模块
        /// </summary>
        [Description("模块配置")]
        Module
    }
}
