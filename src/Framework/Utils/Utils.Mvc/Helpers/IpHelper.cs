using Microsoft.AspNetCore.Http;
using NetModular.Lib.Utils.Core.Attributes;

namespace NetModular.Lib.Utils.Mvc.Helpers
{
    /// <summary>
    /// IP帮助类
    /// </summary>
    [Singleton]
    public class IpHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public IpHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
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
