namespace NetModular.Lib.Pdf.Abstractions
{
    /// <summary>
    /// PDF处理类
    /// </summary>
    public interface IPdfHandler
    {
        /// <summary>
        /// 获取指定pdf文件的页数
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        int GetPages(string filePath);

        /// <summary>
        /// 获取指定PDF文件的文本内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        string GetFullText(string filePath);
    }
}
