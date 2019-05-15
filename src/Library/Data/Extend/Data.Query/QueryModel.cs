namespace NetModular.Lib.Data.Query
{
    /// <summary>
    /// 查询抽象
    /// </summary>
    public class QueryModel
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public QueryPagingModel Page { get; set; } = new QueryPagingModel();

        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount { get; set; }
    }
}
