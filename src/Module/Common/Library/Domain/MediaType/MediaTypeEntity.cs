using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Data.Core.Entities;

namespace Tm.Module.Common.Domain.MediaType
{
    /// <summary>
    /// 多媒体类型
    /// </summary>
    [Table("Media_Type")]
    public class MediaTypeEntity : Entity<int>
    {
        /// <summary>
        /// 后缀名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
