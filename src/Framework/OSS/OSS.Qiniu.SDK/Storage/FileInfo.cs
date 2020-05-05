using Newtonsoft.Json;
namespace Qiniu.Storage
{
    /// <summary>
    /// 获取空间文件信息(stat操作)的有效内容
    /// </summary>
    public class FileInfo
    {
        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [JsonProperty("fsize")]
        public long Fsize { set; get; }

        /// <summary>
        /// 文件hash(ETAG)
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { set; get; }

        /// <summary>
        /// 文件MIME类型
        /// </summary>
        [JsonProperty("mimeType")]
        public string MimeType { set; get; }

        /// <summary>
        /// 文件上传时间
        /// </summary>
        [JsonProperty("putTime")]
        public long PutTime { set; get; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        [JsonProperty("type")]
        public int FileType { get; set; }

    }
}
