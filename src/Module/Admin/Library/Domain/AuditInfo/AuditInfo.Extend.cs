using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.Admin.Domain.AuditInfo
{
    public partial class AuditInfo
    {
        /// <summary>
        /// 操作人
        /// </summary>
        [Ignore]
        public string Creator { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        [Ignore]
        public string ModuleName { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        [Ignore]
        public string PlatformName => Platform.ToDescription();
    }
}
