namespace NetModular.Lib.Data.Query
{
    /// <summary>
    /// 查询抽象
    /// </summary>
    public class QueryModel
    {
        /// <summary>
        /// 总数
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public QueryPagingModel Page { get; set; } = new QueryPagingModel();

        /// <summary>
        /// 导出信息
        /// </summary>
        public ExportModel Export { get; set; }

        /// <summary>
        /// 是否是导出操作
        /// </summary>
        public bool IsExport => Export != null;

        /// <summary>
        /// 导出数量
        /// </summary>
        public long ExportCount { get; set; }

        /// <summary>
        /// 导出数量限制
        /// </summary>
        public virtual int ExportCountLimit => 50000;

        /// <summary>
        /// 是否超出导出数量限制
        /// </summary>
        public bool IsOutOfExportCountLimit => ExportCount > ExportCountLimit;

        /// <summary>
        /// 查询数量
        /// </summary>
        public bool QueryCount { get; set; } = true;
    }
}
