using Newtonsoft.Json;

namespace NetModular.Lib.Config.Abstractions.Impl
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig : IConfig
    {
        /// <summary>
        /// 系统标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Logo文件路径
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 版权声明
        /// </summary>
        public string Copyright { get; set; }
    }
}
