using NetModular.Lib.Utils.Core.Enums;
using NetModular.Lib.Utils.Core.Files;

namespace NetModular.Lib.OSS.Abstractions
{
    /// <summary>
    /// 文件对象
    /// </summary>
    public class FileObject
    {
        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 访问方式
        /// </summary>
        public FileAccessMode AccessMode { get; set; }

        /// <summary>
        /// 文件本地物理路径
        /// </summary>
        public string PhysicalPath { get; set; }

        /// <summary>
        /// 文件信息
        /// </summary>
        public FileInfo FileInfo { get; set; }
    }
}
