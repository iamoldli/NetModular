using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Utils.Core.Extensions;

namespace Tm.Module.Admin.Domain.AuditInfo
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
