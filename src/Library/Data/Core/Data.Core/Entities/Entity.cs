using System;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core.Entities
{
    /// <summary>
    /// 包含指定类型主键的实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class Entity<TKey> : IEntity
    {
        public virtual TKey Id { get; set; }
    }

    /// <summary>
    /// 主键类型为GUID的实体
    /// </summary>
    public class Entity : Entity<Guid>
    {

    }
}
