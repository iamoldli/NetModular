using System.Collections.Generic;

namespace NetModular.Lib.Options.Abstraction
{
    /// <summary>
    /// 配置项持久化提供器接口
    /// </summary>
    public interface IModuleOptionsStorageProvider
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        IList<ModuleOptionStorageModel> GetAll();

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="descriptors">配置描述集合</param>
        /// <returns></returns>
        void Save(List<ModuleOptionStorageModel> descriptors);
    }
}
