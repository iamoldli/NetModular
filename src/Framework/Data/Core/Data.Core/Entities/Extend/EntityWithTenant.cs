using System;
using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Lib.Data.Core.Entities.Extend
{
    /// <summary>
    /// 包含租户信息的实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDeletedByKey"></typeparam>
    public class EntityBaseWithSoftDeleteAndTenant<TKey, TDeletedByKey> : EntityBaseWithSoftDelete<TKey, TDeletedByKey>
    {
        /// <summary>
        /// 租户ID
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 租户名称
        /// </summary>
        [Ignore]
        public string TenantName { get; set; }
    }

    /// <summary>
    /// 包含租户信息的实体
    /// </summary>
    public class EntityBaseWithSoftDeleteAndTenant : EntityBaseWithSoftDeleteAndTenant<Guid, Guid>
    {

    }

}
