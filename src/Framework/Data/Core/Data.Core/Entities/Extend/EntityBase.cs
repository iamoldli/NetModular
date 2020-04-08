using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Abstractions.Entities.Extend;

namespace NetModular.Lib.Data.Core.Entities.Extend
{
    public class EntityBase<TKey> : Entity<TKey>, IBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [Ignore]
        public string Creator { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [Ignore]
        public string Modifier { get; set; }
    }

    public class EntityBase : EntityBase<Guid>
    {

    }
}
