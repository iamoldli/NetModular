using System;
using Nm.Lib.Data.Abstractions.Attributes;

namespace Nm.Lib.Data.Core.Entities.Extend
{

    /// <summary>
    /// 包含指定类型主键的软删除实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDeletedByKey">删除人主键类型</typeparam>
    public class EntityWithSoftDelete<TKey, TDeletedByKey> : Entity<TKey>
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
        public TDeletedByKey DeletedBy { get; set; } = default(TDeletedByKey);

        /// <summary>
        /// 删除人名称
        /// </summary>
        [Ignore]
        public string Deleter { get; set; }
    }

    /// <summary>
    /// 主键类型GUID的软删除实体
    /// </summary>
    public class EntityWithSoftDelete : EntityWithSoftDelete<Guid, Guid>
    {

    }
}
