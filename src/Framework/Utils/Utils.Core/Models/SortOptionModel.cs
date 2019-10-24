namespace Nm.Lib.Utils.Core.Models
{
    /// <summary>
    /// 排序选项模型
    /// </summary>
    public class SortOptionModel<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 附加数据
        /// </summary>
        public object Data { get; set; }
    }
}
