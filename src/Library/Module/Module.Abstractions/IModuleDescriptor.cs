namespace Tm.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块描述
    /// </summary>
    public interface IModuleDescriptor
    {
        /// <summary>
        /// 编号
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// 程序集信息
        /// </summary>
        IModuleAssemblyDescriptor AssemblyDescriptor { get; set; }
    }
}
