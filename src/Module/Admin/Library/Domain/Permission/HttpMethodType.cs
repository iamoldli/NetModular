using System.ComponentModel;

namespace NetModular.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 请求方法类型
    /// </summary>
    public enum HttpMethodType
    {
        [Description("未知")]
        UnKnown,
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
        [Description("OptionOPTIONSs")]
        OPTIONS,
        [Description("TRACE")]
        TRACE,
        [Description("PATCH")]
        PATCH
    }
}
