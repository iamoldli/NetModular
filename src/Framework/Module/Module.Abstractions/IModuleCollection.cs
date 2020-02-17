using System.Collections.Generic;

namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块集合
    /// </summary>
    public interface IModuleCollection : IList<IModuleDescriptor>
    {
        /// <summary>
        /// 加载
        /// </summary>
        void Load();
    }
}