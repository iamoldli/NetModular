using System;
using System.Collections.Generic;
using System.Security.Claims;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Auth.Web;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Module.Admin.Web.Core
{
    [Singleton]
    internal class DefaultLoginClaimsExtendProvider : ILoginClaimsExtendProvider
    {
        public List<Claim> GetExtendClaims(Guid accountId)
        {
            return null;
        }
    }
}
