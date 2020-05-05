using Newtonsoft.Json;

namespace Qiniu.Storage
{
    /// <summary>
    /// 持久化请求的回复
    /// </summary>
    public class PfopInfo 
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        [JsonProperty("id")]
        public string Id;
        /// <summary>
        /// 任务结果状态码
        /// </summary>
        [JsonProperty("code")]
        public int Code;
        /// <summary>
        /// 任务结果状态描述
        /// </summary>
        [JsonProperty("desc")]
        public string Desc;
        /// <summary>
        /// 待处理的数据文件
        /// </summary>
        [JsonProperty("inputKey")]
        public string InputKey;
        /// <summary>
        /// 待处理文件所在空间
        /// </summary>
        [JsonProperty("inputBucket")]
        public string InputBucket;
        /// <summary>
        /// 数据处理队列
        /// </summary>
        [JsonProperty("pipeline")]
        public string Pipeline;
        /// <summary>
        /// 任务的Reqid
        /// </summary>
        [JsonProperty("reqid")]
        public string Reqid;
        /// <summary>
        /// 数据处理的命令集合
        /// </summary>
        [JsonProperty("items")]
        public PfopItems[] Items;
    }

    /// <summary>
    /// 持久化处理命令
    /// </summary>
    public class PfopItems
    {
        /// <summary>
        /// 命令
        /// </summary>
        [JsonProperty("cmd")]
        public string Cmd;
        /// <summary>
        /// 命令执行结果状态码
        /// </summary>
        [JsonProperty("code")]
        public string Code;
        /// <summary>
        /// 命令执行结果描述
        /// </summary>
        [JsonProperty("desc")]
        public string Desc;
        /// <summary>
        /// 命令执行错误
        /// </summary>
        [JsonProperty("Error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error;
        /// <summary>
        /// VSample命令的生成文件名列表
        /// </summary>
        [JsonProperty("keys", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Keys;
        /// <summary>
        /// 命令生成的文件名
        /// </summary>
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key;
        /// <summary>
        /// 命令生成的文件内容hash
        /// </summary>
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash;
        /// <summary>
        /// 该命令是否返回了上一次相同命令生成的结果
        /// </summary>
        [JsonProperty("returnOld", NullValueHandling = NullValueHandling.Ignore)]
        public int? ReturnOld;
    }
}
