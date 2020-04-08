using System;
using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Lib.Data.Abstractions.Entities.Extend
{
    /// <summary>
    /// 软删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 已删除
        /// </summary>
        bool Deleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime DeletedTime { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>
        [Nullable]
        Guid DeletedBy { get; set; }

        /// <summary>
        /// 删除人名称
        /// </summary>
        [Ignore]
        string Deleter { get; set; }
    }
}
