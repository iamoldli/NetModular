namespace NetModular.Lib.WebHost.Core.Options
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
        /// 开启Swagger
        /// </summary>
        public bool Swagger { get; set; }
    }
}
