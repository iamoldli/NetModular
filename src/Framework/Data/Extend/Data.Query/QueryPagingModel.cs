using System.Collections.Generic;

namespace NetModular.Lib.Data.Query
{
    /// <summary>
    /// 查询分页模型
    /// </summary>
    public class QueryPagingModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Index { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; } = 15;

        /// <summary>
        /// 排序字段
        /// </summary>
        public List<QuerySortModel> Sort { get; set; }
    }
}
