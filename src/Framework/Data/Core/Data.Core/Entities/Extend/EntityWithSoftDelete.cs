using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities.Extend;

namespace NetModular.Lib.Data.Core.Entities.Extend
{
    /// <summary>
    /// 包含指定类型主键的软删除实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityWithSoftDelete<TKey> : Entity<TKey>, ISoftDelete
    {
        /// <summary>
        /// 已删除
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeletedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 删除人
        /// </summary>
        [Nullable]
        public Guid DeletedBy { get; set; } = Guid.Empty;

        /// <summary>
        /// 删除人名称
        /// </summary>
        [Ignore]
        public string Deleter { get; set; }
    }

    /// <summary>
    /// 主键类型GUID的软删除实体
    /// </summary>
    public class EntityWithSoftDelete : EntityWithSoftDelete<Guid>
    {

    }
}
