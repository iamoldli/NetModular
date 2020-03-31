using NetModular.Lib.Data.Abstractions.Attributes;

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
