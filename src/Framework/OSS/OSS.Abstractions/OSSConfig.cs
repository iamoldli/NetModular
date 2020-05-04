namespace NetModular.Lib.OSS.Abstractions
{
    /// <summary>
    /// OSSConfig
    /// </summary>
    public class OSSConfig
    {
        /// <summary>
        /// OSS提供器
        /// </summary>
        public OSSProvider Provider { get; set; } = OSSProvider.Local;

        /// <summary>
        /// 七牛配置
        /// </summary>
        public QiniuConfig Qiniu { get; set; } = new QiniuConfig();
    }
}
