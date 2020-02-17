using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Admin.Domain.Config
{
    /// <summary>
    /// 配置项
    /// </summary>
    [Table("Config")]
    public partial class ConfigEntity : EntityBase<int>
    {
        /// <summary>
        /// 类型
        /// </summary>
        public ConfigType Type { get; set; }

        /// <summary>
        /// 键名
        /// </summary>
        [Length(300)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Length(1000)]
        [Nullable]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Length(300)]
        [Nullable]
        public string Remarks { get; set; }
    }
}
