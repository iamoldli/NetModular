using System.Threading.Tasks;

namespace NetModular.Lib.Config.Abstraction
{
    /// <summary>
    /// 配置容器
    /// </summary>
    public interface IConfigContainer
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> Resolve<T>() where T : IConfig, new();

        /// <summary>
        /// 根据键删除配置项实例
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(string key);

        /// <summary>
        /// 刷新所有数据
        /// </summary>
        /// <returns></returns>
        Task RefreshAll();
    }
}
