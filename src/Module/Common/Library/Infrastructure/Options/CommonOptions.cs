using Nm.Lib.Utils.Core.Options;

namespace Nm.Module.Common.Infrastructure.Options
{
    /// <summary>
    /// 通用模块配置项
    /// </summary>
    public class CommonOptions : IModuleOptions
    {
        /// <summary>
        /// 附件上传路径
        /// </summary>
        public string AttachmentPath { get; set; }
    }
}
