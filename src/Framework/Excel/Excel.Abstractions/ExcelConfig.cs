using NetModular.Lib.Config.Abstractions;

namespace NetModular.Lib.Excel.Abstractions
{
    /// <summary>
    /// Excel操作配置项
    /// </summary>
    public class ExcelConfig : IConfig
    {
        /// <summary>
        /// Excel操作时产生的临时文件存储根路径
        /// </summary>
        public string TempPath { get; set; }
    }
}
