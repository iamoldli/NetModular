using System.Web;

namespace NetModular.Lib.Utils.Core.Result
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class FileDownloadModel
    {
        /// <summary>
        /// 文件完整路径
        /// </summary>
        public string FilePath { get; set; }

        private string _name;
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name
        {
            get => HttpUtility.UrlEncode(_name);
            set => _name = value;
        }

        /// <summary>
        /// 多媒体类型
        /// </summary>
        public string MediaType { get; set; }

        public FileDownloadModel(string filePath, string name, string mediaType)
        {
            FilePath = filePath;
            Name = name;
            MediaType = mediaType ?? "";
        }
    }
}
