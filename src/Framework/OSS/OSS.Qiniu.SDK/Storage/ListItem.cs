using Newtonsoft.Json;
namespace Qiniu.Storage
{
    /// <summary>
    /// 文件描述(stat操作返回消息中包含的有效内容)
    /// 与StatInfo一致
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// 文件名
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 文件hash(ETAG)
        /// </summary>
        [JsonProperty("hash")]
        public string Hash { get; set; }

        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [JsonProperty("fsize")]
        public long Fsize { get; set; }

        /// <summary>
        /// 文件MIME类型
        /// </summary>
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonProperty("putTime")]
        public long PutTime { get; set; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        [JsonProperty("type")]
        public int FileType { get; set; }

        /// <summary>
        /// EndUser字段
        /// </summary>
        [JsonProperty("endUser")]
        public string EndUser { get; set; }
    }
}
