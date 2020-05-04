using System.ComponentModel;

namespace NetModular.Lib.OSS.Abstractions
{
    /// <summary>
    /// OSS提供器
    /// </summary>
    public enum OSSProvider
    {
        [Description("本地存储")]
        Local,
        /// <summary>
        /// 七牛
        /// </summary>
        [Description("七牛")]
        Qiniu
    }
}
