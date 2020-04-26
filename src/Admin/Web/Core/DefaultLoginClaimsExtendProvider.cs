using System.Collections.Generic;
using System.Security.Claims;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Module.Admin.Domain.Account;

namespace NetModular.Module.Admin.Web.Core
{
    [Singleton]
    internal class DefaultLoginClaimsExtendProvider : ILoginClaimsExtendProvider
    {
        public List<Claim> GetExtendClaims(AccountEntity account)
        {
            return null;
        }
    }
}
