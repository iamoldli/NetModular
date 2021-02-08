using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 登录信息
    /// </summary>
    [Singleton]
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid? TenantId
        {
            get
            {
                var tenantId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.TenantId);

                if (tenantId != null && tenantId.Value.NotNull())
                {
                    return new Guid(tenantId.Value);
                }

                return null;
            }
        }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountId);

                if (accountId != null && accountId.Value.NotNull())
                {
                    return new Guid(accountId.Value);
                }

                return Guid.Empty;
            }
        }

        public string AccountName
        {
            get
            {
                var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountName);

                if (accountName == null || accountName.Value.IsNull())
                {
                    return string.Empty;
                }

                return accountName.Value;
            }
        }

        /// <summary>
        /// 请求平台
        /// </summary>
        public Platform Platform
        {
            get
            {
                var pt = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.Platform);

                if (pt != null && pt.Value.NotNull())
                {
                    return (Platform)pt.Value.ToInt();
                }

                return Platform.UnKnown;
            }
        }

        public AccountType AccountType
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.AccountType);

                if (ty != null && ty.Value.NotNull())
                {
                    return (AccountType)ty.Value.ToInt();
                }

                return AccountType.UnKnown;
            }
        }

        private IPAddress _ip
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Request?.Headers != null)
                {
                    var headers = _contextAccessor.HttpContext.Request.Headers;

                    List<string> headerKeys = new List<string>
                    {
                        "X-FORWARDED-FOR",
                        "HTTP-X-FORWARDED-FOR",
                        "REMOTE-ADDR",
                        "X-REAl-IP"
                    };
                    headerKeys.AddRange(headerKeys.Select(m => m.Replace("-", "_")));
                    foreach (var headerKey in headerKeys)
                    {
                        if (headers.TryGetValue(headerKey, out StringValues ips))
                        {
                            foreach (var ip in ips)
                            {
                                if (IPAddress.TryParse(ip, out IPAddress addr))
                                {
                                    return addr;
                                }

                                var ipParts = ip.Split(":");
                                if (ipParts.Length == 2)
                                {
                                    if (IPAddress.TryParse(ipParts[0], out addr))
                                    {
                                        return addr;
                                    }
                                }
                            }
                        }
                    }
                }

                if (_contextAccessor?.HttpContext?.Connection != null)
                {
                    var connection = _contextAccessor.HttpContext.Connection;
                    return connection.RemoteIpAddress;
                }

                return null;
            }
        }

        /// <summary>
        /// 获取当前用户IP(包含IPv4和IPv6)
        /// </summary>
        public string IP
        {
            get
            {
                if (_ip == null)
                {
                    return string.Empty;
                }

                return _ip.ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        public string IPv4
        {
            get
            {
                if (_ip == null)
                {
                    return string.Empty;
                }

                return _ip.MapToIPv4().ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        public string IPv6
        {
            get
            {
                if (_ip == null)
                {
                    return string.Empty;
                }

                return _ip.MapToIPv6().ToString();
            }
        }

        /// <summary>
        /// 登录时间
        /// </summary>
        public long LoginTime
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.LoginTime);

                if (ty != null && ty.Value.NotNull())
                {
                    return ty.Value.ToLong();
                }

                return 0L;
            }
        }

        /// <summary>
        /// User-Agent
        /// </summary>
        public string UserAgent
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Request == null)
                {
                    return string.Empty;
                }

                return _contextAccessor.HttpContext.Request.Headers["User-Agent"];
            }
        }
    }
}
