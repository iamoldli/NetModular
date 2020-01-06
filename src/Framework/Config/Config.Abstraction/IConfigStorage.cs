using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Lib.Config.Abstraction
{
    /// <summary>
    /// 配置项提供接口
    /// </summary>
    public interface IConfigStorage
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        Task<IList<ConfigDescriptor>> GetAll();

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        Task Add(ConfigDescriptor descriptor);
    }
}
