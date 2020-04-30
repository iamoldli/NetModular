using NetModular.Lib.Data.Query;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Module.Admin.Domain.File.Models
{
    public class FileQueryModel : QueryModel
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 访问方式
        /// </summary>
        public FileAccessMode? AccessMode { get; set; }
    }
}
