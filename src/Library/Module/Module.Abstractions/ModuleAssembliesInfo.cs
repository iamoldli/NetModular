using System.Reflection;

namespace NetModular.Lib.Module.Abstractions
{
    /// <summary>
    /// 模块包含的程序集
    /// </summary>
    public class ModuleAssembliesInfo
    {
        /// <summary>
        /// Web
        /// </summary>
        public Assembly Web { get; set; }

        /// <summary>
        /// 应用层服务
        /// </summary>
        public Assembly Application { get; set; }

        /// <summary>
        /// 领域
        /// </summary>
        public Assembly Domain { get; set; }

        /// <summary>
        /// 基础设施
        /// </summary>
        public Assembly Infrastructure { get; set; }
    }
}
