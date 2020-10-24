using System;
using Microsoft.AspNetCore.Http;

namespace NetModular.Lib.Utils.Mvc.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static Guid GetClaim2Guid(this IHttpContextAccessor accessor,string type)
        {
            var claim = accessor?.HttpContext?.User?.FindFirst(type);

            if (claim != null && claim.Value.NotNull())
            {
                return new Guid(claim.Value);
            }

            return Guid.Empty;
        }

        public static int GetClaim2Int(this IHttpContextAccessor accessor, string type)
        {
            var claim = accessor?.HttpContext?.User?.FindFirst(type);

            if (claim != null && claim.Value.NotNull())
            {
                return claim.Value.ToInt();
            }

            return 0;
        }

        public static long GetClaim2Long(this IHttpContextAccessor accessor, string type)
        {
            var claim = accessor?.HttpContext?.User?.FindFirst(type);

            if (claim != null && claim.Value.NotNull())
            {
                return claim.Value.ToLong();
            }

            return 0L;
        }

        public static string GetClaim2String(this IHttpContextAccessor accessor, string type)
        {
            var claim = accessor?.HttpContext?.User?.FindFirst(type);

            if (claim != null && claim.Value.NotNull())
            {
                return claim.Value;
            }

            return string.Empty;
        }
    }
}
