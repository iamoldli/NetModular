using System.Collections.Generic;
using NetModular.Lib.Module.Abstractions;

namespace NetModular.Lib.Quartz.Abstractions
{
    /// <summary>
    /// 调度模块描述信息
    /// </summary>
    public class QuartzModuleDescriptor
    {
        /// <summary>
        /// 模块信息
        /// </summary>
        public IModuleDescriptor Module { get; set; }

        /// <summary>
        /// 任务列表
        /// </summary>
        public List<QuartzTaskDescriptor> Tasks { get; } = new List<QuartzTaskDescriptor>();

        /// <summary>
        /// 任务下拉列表
        /// </summary>
        public List<OptionResultModel> TaskSelect { get; } = new List<OptionResultModel>();
    }
}
