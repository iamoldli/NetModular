using System.Collections.Generic;
using System.ComponentModel;
using NetModular.Lib.Utils.Core.Models;

namespace NetModular.Lib.Data.Query
{
    /// <summary>
    /// 导出模型
    /// </summary>
    public class ExportModel
    {
        /// <summary>
        /// 是否显示标题
        /// </summary>
        public bool ShowTitle { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 导出格式
        /// </summary>
        public ExportFormat Format { get; set; }

        /// <summary>
        /// 导出模式
        /// </summary>
        public ExportMode Mode { get; set; }

        /// <summary>
        /// 是否显示版权信息
        /// </summary>
        public bool ShowCopyright { get; set; }

        /// <summary>
        /// 显示列名称
        /// </summary>
        public bool ShowColName { get; set; }

        /// <summary>
        /// 显示导出日期
        /// </summary>
        public bool ShowExportDate { get; set; }

        /// <summary>
        /// 显示导出人
        /// </summary>
        public bool ShowExportPeople { get; set; }

        /// <summary>
        /// 导出的列
        /// </summary>
        public IList<ColumnModel> Columns { get; set; }
    }

    /// <summary>
    /// 导出格式
    /// </summary>
    public enum ExportFormat
    {
        [Description(".xlsx")]
        Xlsx
    }

    /// <summary>
    /// 导出模式
    /// </summary>
    public enum ExportMode
    {
        [Description("全部")]
        All,
        [Description("当前页")]
        CurrentPage
    }
}
