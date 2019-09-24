using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Common.Domain.Attachment
{
    /// <summary>
    /// 附件表
    /// </summary>
    [Table("Attachment")]
    public partial class AttachmentEntity : EntityBaseWithSoftDelete
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        [Length(100)]
        public string Group { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        [Length(100)]
        public string FileName { get; set; }

        /// <summary>
        /// 存储名称
        /// </summary>
        public string SaveName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 大小(字节)
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件大小中文名
        /// </summary>
        [Length(20)]
        public string SizeCn { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        [Length(200)]
        public string Path { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        [Length(300)]
        public string FullPath { get; set; }

        /// <summary>
        /// 是否需要授权访问
        /// </summary>
        public bool Auth { get; set; }

        /// <summary>
        /// 多媒体类型
        /// </summary>
        [Length(100)]
        public string MediaType { get; set; }
    }
}
