using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Lib.Auth.Web.TenantResolvers
{
    /// <summary>
    /// 租户解析默认实现
    /// </summary>
    public class DefaultTenantResolver : ITenantResolver
    {
        public int Resolve()
        {
            return 0;
        }
    }
}
