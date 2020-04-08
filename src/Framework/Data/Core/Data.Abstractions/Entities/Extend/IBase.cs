using System;

namespace NetModular.Lib.Data.Abstractions.Entities.Extend
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public interface IBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        Guid CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime ModifiedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        Guid ModifiedBy { get; set; }
    }
}
