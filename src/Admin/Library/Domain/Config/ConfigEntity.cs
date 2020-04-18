using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Table("Config")]
    public partial class ConfigEntity : Entity<int>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ConfigType Type { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Max]
        [Nullable]
        public string Value { get; set; }
    }
}
