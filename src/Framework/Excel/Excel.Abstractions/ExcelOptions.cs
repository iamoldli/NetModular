namespace NetModular.Lib.Excel.Abstractions
{
    /// <summary>
    /// Excel操作配置项
    /// </summary>
    public class ExcelOptions
    {
        public ExcelProvider Provider { get; set; }

        /// <summary>
        /// Excel操作时产生的临时文件存储根路径
        /// </summary>
        public string TempPath { get; set; }
    }
}
