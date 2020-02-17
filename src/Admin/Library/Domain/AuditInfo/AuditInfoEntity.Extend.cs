using NetModular.Lib.Data.Abstractions.Attributes;
using NetModular.Lib.Utils.Core.Extensions;

namespace NetModular.Module.Admin.Domain.AuditInfo
{
    public partial class AuditInfoEntity
    {
        /// <summary>
        /// 平台名称
        /// </summary>
        [Ignore]
        public string PlatformName => Platform.ToDescription();
    }
}
