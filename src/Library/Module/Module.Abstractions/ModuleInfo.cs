namespace Nm.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 模块初始化器
        /// </summary>
        public IModuleInitializer Initializer { get; set; }

        /// <summary>
        /// 程序集信息
        /// </summary>
        public ModuleAssembliesInfo AssembliesInfo { get; set; }
    }
}
