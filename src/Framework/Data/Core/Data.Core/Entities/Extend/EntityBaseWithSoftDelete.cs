using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities.Extend;

namespace NetModular.Lib.Data.Core.Entities.Extend
{
    /// <summary>
    /// 软删除实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class EntityBaseWithSoftDelete<TKey> : EntityBase<TKey>, ISoftDelete
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
    /// 包含软删除功能的实体基类
    /// </summary>
    public class EntityBaseWithSoftDelete : EntityBaseWithSoftDelete<Guid>
    {

    }
}
