using System.Collections.Generic;

namespace Qiniu.CDN
{
    /// <summary>
    /// 文件预取-消息内容结构
    /// 
    /// 在请求成功时 code 为 200，requestId、quotaDay、surplusDay 才会有有效值，否则为空。
    /// 在请求失败时 code 为非 200，error 中包含描述信息。
    /// 
    /// 以下是一个返回结果示例
    /// 
    /// {
    ///     "code":200,
    ///     "error":"success",
    ///     "requestId":"577471ace3ab3a030c058972",
    ///     "invalidUrls":null,
    ///     "quotaDay":100,
    ///     "surplusDay":99
    /// }
    /// 
    /// </summary>
    public class PrefetchInfo
    {
        /// <summary>
        /// 代码	含义	说明
        /// /// 200	success	成功(OK)
        /// 400031	invalid url 请求中存在无效的 url，请确保提交的 url 格式正确
        /// 400032	invalid host    请求中存在无效的域名，请确保域名格式正确
        /// 400033	prefetch url limit error    请求次数超出当日预取限额
        /// 400036	invalid request id 无效的请求 id
        /// 400037	url has existed url 正在预取中
        /// 500000	internal error 服务端内部错误，请联系技术支持
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息(状态码非OK时)
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 请求ID(可用于反馈排查)
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 非法URL
        /// </summary>
        public List<string> InvalidUrls { get; set; }

        /// <summary>
        /// 当日限额
        /// </summary>
        public int QuotaDay { get; set; }

        /// <summary>
        /// 当日剩余额度
        /// </summary>
        public int SurplusDay { get; set; }
    }
}
