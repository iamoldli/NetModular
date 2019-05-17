using System.Collections.Generic;

namespace Nm.Lib.Utils.Core.Models
{
    /// <summary>
    /// 排序更新模型
    /// </summary>
    public class SortUpdateModel<TKey>
    {
        /// <summary>
        /// 选项列表
        /// </summary>
        public IList<SortOptionModel<TKey>> Options { get; set; }
    }
}
