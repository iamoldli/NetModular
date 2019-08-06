using System;
using Microsoft.AspNetCore.Http;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Lib.Auth.Web
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public class LoginInfo : ILoginInfo
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginInfo(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        /// <summary>
        /// 账户编号
        /// </summary>
        public Guid AccountId
        {
            get
            {
                var accountId = _contextAccessor?.HttpContext?.User?.FindFirst("id");

                if (accountId != null && accountId.Value.NotNull())
                {
                    return new Guid(accountId.Value);
                }

                return Guid.Empty;
            }
        }

        /// <summary>
        /// 请求平台
        /// </summary>
        public Platform Platform
        {
            get
            {
                if (_contextAccessor?.HttpContext?.User == null)
                    return Platform.UnKnown;

                var pt = _contextAccessor.HttpContext.User.FindFirst("pf");
                if (pt != null && pt.Value.NotNull())
                {
                    return (Platform)pt.Value.ToInt();
                }

                return Platform.Web;
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
    }
}
