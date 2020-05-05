using Newtonsoft.Json;
namespace Qiniu.Storage
{
    /// <summary>
    /// 分片上传的记录信息
    /// </summary>
    public class ResumeInfo
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// 当前块编号
        /// </summary>
        [JsonProperty("blockIndex")]
        public int BlockIndex { get; set; }

        /// <summary>
        /// 文件块总数
        /// </summary>
        [JsonProperty("blockCount")]
        public int BlockCount { get; set; }

        /// <summary>
        /// 上下文信息列表
        /// </summary>
        [JsonProperty("contexts")]
        public string[] Contexts { get; set; }

        /// <summary>
        /// Ctx过期时间戳（单位秒）
        /// </summary>
        [JsonProperty("expiredAt")]
        public long ExpiredAt { get; set; }

        /// <summary>
        /// 上传进度信息序列化
        /// </summary>
        /// <returns></returns>
        public string ToJsonStr()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
