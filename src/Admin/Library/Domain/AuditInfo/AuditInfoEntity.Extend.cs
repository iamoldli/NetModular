using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Module.Admin.Domain.AuditInfo
{
    public partial class AuditInfoEntity
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
