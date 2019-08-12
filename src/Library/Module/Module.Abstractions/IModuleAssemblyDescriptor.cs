using System.Reflection;

namespace Tm.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块的程序集描述
    /// </summary>
    public interface IModuleAssemblyDescriptor
    {
        /// <summary>
        /// 应用层服务
        /// </summary>
        Assembly Application { get; set; }

        /// <summary>
        /// 领域
        /// </summary>
        Assembly Domain { get; set; }

        /// <summary>
        /// 基础设施
        /// </summary>
        Assembly Infrastructure { get; set; }

        /// <summary>
        /// 任务调度Quartz
        /// </summary>
        Assembly Quartz { get; set; }
    }
}
