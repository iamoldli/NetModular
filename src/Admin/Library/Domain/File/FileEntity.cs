using System;
using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Data.Core.Entities.Extend;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Module.Admin.Domain.File
{
    /// <summary>
    /// 文件信息
    /// </summary>
    [Table("File")]
    public partial class FileEntity : EntityBaseWithSoftDelete<int, Guid>
    {
        /// <summary>
        /// 存储编号
        /// </summary>
        public string SaveId { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        [Length(100)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Length(100)]
        public string FileName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 大小(字节)
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件大小名称
        /// </summary>
        public string SizeName { get; set; }

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
        /// MIME类型
        /// </summary>
        [Length(100)]
        [Nullable]
        public string Mime { get; set; }

        /// <summary>
        /// 访问方式
        /// </summary>
        public FileAccessMode AccessMode { get; set; }
    }
}
