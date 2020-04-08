using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities;

namespace NetModular.Module.Admin.Domain.Module
{
    /// <summary>
    /// 模块
    /// </summary>
    [Table("Module")]
    [Tenant]
    public partial class ModuleEntity : Entity<int>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Nullable]
        public string Icon { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Length(300)]
        [Nullable]
        public string Remarks { get; set; }

        /// <summary>
        /// 接口请求数量
        /// </summary>
        public long ApiRequestCount { get; set; }
    }
}
