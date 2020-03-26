
using System;

namespace NetModular.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 多租户实体
    /// </summary>
    public interface IMultiTenant
    {
        /// <summary>
        /// 多租户Id
        /// </summary>
        Guid? TenantId { get; }
    }
}
