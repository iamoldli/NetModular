using System.Collections.Generic;

namespace NetModular.Lib.Cache.Abstractions
{
    /// <summary>
    /// 缓存键信息集合
    /// </summary>
    public interface ICacheKeyDescriptorCollection : IList<CacheKeyDescriptor>
    {
        /// <summary>
        /// 获取指定模块中的所有缓存键
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        IEnumerable<CacheKeyDescriptor> GetByModule(string moduleCode);
    }
}
