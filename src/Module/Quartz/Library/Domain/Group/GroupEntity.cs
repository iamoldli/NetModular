using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Quartz.Domain.Group
{
    /// <summary>
    /// 任务组
    /// </summary>
    [Table("Group")]
    public partial class GroupEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Length(100)]
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Length(100)]
        public string Code { get; set; }
    }
}
