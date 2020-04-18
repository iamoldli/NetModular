using System;
using Microsoft.AspNetCore.Http;
using NetModular.Lib.Auth.Abstractions;

namespace NetModular.Lib.Auth.Web
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITenantResolver _tenantResolver;

        public LoginInfo(IHttpContextAccessor contextAccessor, ITenantResolver tenantResolver)
        {
            _contextAccessor = contextAccessor;
            _tenantResolver = tenantResolver;
        }
        
        /// <summary>
        /// 租户编号
        /// </summary>
        public int TenantId => _tenantResolver.Resolve();

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.ACCOUNT_ID);

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
                var accountName = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.ACCOUNT_NAME);

                if (accountName == null || accountName.Value.IsNull())
                {
                    return "";
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
                var pt = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.PLATFORM);
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
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.ACCOUNT_TYPE);

                if (ty != null && ty.Value.NotNull())
                {
                    return (AccountType)ty.Value.ToInt();
                }

                return AccountType.UnKnown;
            }
        }

        /// <summary>
        /// 获取当前用户IP(包含IPv和IPv6)
        /// </summary>
        public string IP
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv4
        /// </summary>
        public string IPv4
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }
        }

        /// <summary>
        /// 获取当前用户IPv6
        /// </summary>
        public string IPv6
        {
            get
            {
                if (_contextAccessor?.HttpContext?.Connection == null)
                    return "";

                return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv6().ToString();
            }
        }

        /// <summary>
        /// 登录时间
        /// </summary>
        public long LoginTime
        {
            get
            {
                var ty = _contextAccessor?.HttpContext?.User?.FindFirst(ClaimsName.LOGIN_TIME);

                if (ty != null && ty.Value.NotNull())
                {
                    return ty.Value.ToLong();
                }

                return 0L;
            }
        }
    }
}
