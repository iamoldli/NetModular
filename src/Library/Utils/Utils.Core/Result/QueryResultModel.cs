using System.Collections.Generic;

namespace Nm.Lib.Utils.Core.Result
{
    /// <summary>
    /// 查询结果模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryResultModel<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public IList<T> Rows { get; set; }
    }
}
