using Newtonsoft.Json;
namespace Qiniu.Storage
{
    /// <summary>
    /// 批量处理返回的信息
    /// </summary>
    public class BatchInfo
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("code",NullValueHandling=NullValueHandling.Ignore)]
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [JsonProperty("data",NullValueHandling=NullValueHandling.Ignore)]
        public BatchData Data { get; set; }
    }

    /// <summary>
    /// 批量处理的结果内容
    /// </summary>
    public class BatchData
    {
        /// <summary>
        /// 处理遇到的错误信息
        /// </summary>
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        /// 文件hash(ETAG)
        /// </summary>
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash { get; set; }

        /// <summary>
        /// 文件大小(字节)
        /// </summary>
        [JsonProperty("fsize", NullValueHandling = NullValueHandling.Ignore)]
        public long Fsize { get; set; }

        /// <summary>
        /// 文件MIME类型
        /// </summary>
        [JsonProperty("mimeType", NullValueHandling = NullValueHandling.Ignore)]
        public string MimeType { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [JsonProperty("putTime", NullValueHandling = NullValueHandling.Ignore)]
        public long PutTime { get; set; }

        /// <summary>
        /// 文件存储类型
        /// </summary>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public int FileType { get; set; }
    }
}
