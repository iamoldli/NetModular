namespace NetModular.Lib.Pdf.Abstractions
{
    public class PdfConfig
    {
        /// <summary>
        /// 提供器
        /// </summary>
        public PdfProvider Provider { get; set; }

        /// <summary>
        /// 操作时产生的临时文件存储根路径
        /// </summary>
        public string TempPath { get; set; }
    }
}
