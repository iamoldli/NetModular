using NetModular.Lib.Data.Abstractions.Attributes;

namespace NetModular.Module.Admin.Domain.LoginLog
{
    public partial class LoginLogEntity
    {
        [Ignore]
        public string PlatformName => Platform.ToDescription();

        [Ignore]
        public string LoginModeName => LoginMode.ToDescription();
    }
}
