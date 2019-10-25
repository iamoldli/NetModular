using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;

namespace NetModular.Module.Admin.Domain.ModuleInfo
{
    /// <summary>
    /// 模块信息
    /// </summary>
    [Table("ModuleInfo")]
    public class ModuleInfoEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Length(200)]
        [Nullable]
        public string Remarks { get; set; }
    }
}
