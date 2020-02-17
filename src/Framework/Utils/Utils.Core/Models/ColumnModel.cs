using System.ComponentModel;
using System.Reflection;

namespace NetModular.Lib.Utils.Core.Models
{
    /// <summary>
    /// 列模型
    /// </summary>
    public class ColumnModel
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public ColumnAlign Align { get; set; }

        /// <summary>
        /// 格式化，暂时仅支持日期类型
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
    }

    /// <summary>
    /// 列对齐方式
    /// </summary>
    public enum ColumnAlign
    {
        [Description("Left")]
        Left,
        [Description("Center")]
        Center,
        [Description("Right")]
        Right
    }
}
