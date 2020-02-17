namespace NetModular.Lib.Utils.Core.Result
{
    /// <summary>
    /// 图表数据返回结果
    /// </summary>
    public class ChartDataResultModel<TKey>
    {
        /// <summary>
        /// 键
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// 图表数据返回结果
    /// </summary>
    public class ChartDataResultModel : ChartDataResultModel<string>
    {

    }
}
