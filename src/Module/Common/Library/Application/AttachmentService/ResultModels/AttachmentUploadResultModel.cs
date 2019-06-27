using System;

namespace Nm.Module.Common.Application.AttachmentService.ResultModels
{
    public class AttachmentUploadResultModel
    {
        /// <summary>
        /// 附件编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
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
        public string SizeCn { get; set; }

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }
    }
}
