namespace NetModular.Lib.Host.Web.Options
{
    /// <summary>
    /// 主机配置项
    /// </summary>
    public class HostOptions
    {
        /// <summary>
        /// 绑定的地址(默认：http://*:5000)
        /// </summary>
        public string Urls { get; set; }

        /// <summary>
        /// 基础地址(默认：/)
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 开启Swagger
        /// </summary>
        public bool Swagger { get; set; }

        /// <summary>
        /// 启用代理
        /// </summary>
        public bool Proxy { get; set; }

        /// <summary>
        /// 指定跨域访问时预检请求的有效期，单位秒，默认30分钟
        /// </summary>
        public int PreflightMaxAge { get; set; }

        /// <summary>
        /// 隐藏启动Logo
        /// </summary>
        public bool HideStartLogo { get; set; }
    }
}
