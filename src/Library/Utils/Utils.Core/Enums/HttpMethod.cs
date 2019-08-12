using System.ComponentModel;

namespace Tm.Lib.Utils.Core.Enums
{
    /// <summary>
    /// 请求方法类型
    /// </summary>
    public enum HttpMethod
    {
        [Description("GET")]
        GET,
        [Description("PUT")]
        PUT,
        [Description("POST")]
        POST,
        [Description("DELETE")]
        DELETE,
        [Description("HEAD")]
        HEAD,
        [Description("OPTIONS")]
        OPTIONS,
        [Description("TRACE")]
        TRACE,
        [Description("PATCH")]
        PATCH
    }
}
