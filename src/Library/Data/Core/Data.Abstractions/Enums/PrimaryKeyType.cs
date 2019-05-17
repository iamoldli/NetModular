using System.ComponentModel;

namespace Nm.Lib.Data.Abstractions.Enums
{
    /// <summary>
    /// 主键类型
    /// </summary>
    public enum PrimaryKeyType
    {
        /// <summary>
        /// 没有主键
        /// </summary>
        [Description("无")]
        NoPrimaryKey,
        /// <summary>
        /// 整型
        /// </summary>
        [Description("Int")]
        Int,
        /// <summary>
        /// 长整型
        /// </summary>
        [Description("Long")]
        Long,
        /// <summary>
        /// 全球唯一码
        /// </summary>
        [Description("Guid")]
        Guid
    }
}
