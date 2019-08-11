using System.Collections.Generic;
using Nm.Lib.Module.Abstractions;
using Nm.Lib.Utils.Core.Result;

namespace Nm.Module.Quartz.Web.Core
{
    /// <summary>
    /// 任务模块描述类
    /// </summary>
    public class JobModuleDescriptor
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IModuleDescriptor Descriptor { get; set; }

        /// <summary>
        /// 任务类下拉列表
        /// </summary>
        public IList<OptionResultModel> JobClassSelect { get; set; }
    }
}
