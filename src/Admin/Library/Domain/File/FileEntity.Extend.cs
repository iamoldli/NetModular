using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Admin.Domain.File
{
    public partial class FileEntity
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Ignore]
        public string ModuleName { get; set; }

        /// <summary>
        /// 访问URL
        /// </summary>
        [Ignore]
        public string Url { get; set; }

        [Ignore]
        public string AccessModeName => AccessMode.ToDescription();
    }
}
