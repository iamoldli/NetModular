using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Abstractions.LoginModels;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Auth.Web
{
    [Singleton]
    public class DefaultTenantResolver : ITenantResolver
    {
        public Task Resolve(LoginResultModel loginResultModel)
        {
            loginResultModel.TenantId = null;
            return Task.CompletedTask;
        }
    }
}
