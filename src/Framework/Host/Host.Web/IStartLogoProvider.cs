using NetModular.Lib.Host.Web.Options;

namespace NetModular.Lib.Host.Web
{
    /// <summary>
    /// 启动logo提供器
    /// </summary>
    public interface IStartLogoProvider
    {
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="options"></param>
        void Show(HostOptions options);
    }
}
