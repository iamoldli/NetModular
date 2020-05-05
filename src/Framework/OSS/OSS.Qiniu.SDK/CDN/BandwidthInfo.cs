using System.Collections.Generic;
using System;
namespace Qiniu.CDN
{
    /// <summary>
    /// 带宽-消息内容结构
    /// 说明：
    /// 1.返回的数据包含开始日期和结束日期
    /// 2.带宽的单位为 bps
    /// 3.数据(data)只包含有流量的域名
    /// 以下是一个返回结果示例
    /// 
    /// 200 OK HTTP/1.1 
    /// {
    ///     "code": 200,
    ///     "error": "",
    ///     "time": ["2016-07-01 00:00:00","2016-07-01 00:05:00", ...],
    ///     "data": {
    ///         "a.com": {
    ///             "china": [8888, 9999, 10000, ...],
    ///             "oversea": [3333, 4444, 5000, ...],
    ///             },
    ///         "b.com": {
    ///             "china": [8888, 9999, 10000, ...],
    ///             "oversea": [3333, 4444, 5000, ...],
    ///             }
    ///         }
    /// }
    /// 
    /// 另请参阅 http://developer.qiniu.com/article/fusion/api/traffic-bandwidth.html#batch-bandwidth
    /// </summary>
    public class BandwidthInfo
    {
        /// <summary>
        /// 代码 含义 说明
        /// 200	success	成功(OK)
        /// 400032	invalid host    请求中存在无效的域名，请确保域名格式正确
        /// 400080	invalid start time 开始时间格式错误
        /// 400081	invalid end time 截止时间格式错误
        /// 400082	invalid time range 时间范围错误，请确保开始时间早于结束时间，且时间范围不超过 30 天
        /// 500000	internal error 服务端内部错误，请联系技术支持
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息(状态码非OK时)
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 时间点列表
        /// </summary>
        public List<string> Time { get; set; }

        /// <summary>
        /// 带宽数居(与时间点列表对应)
        /// 数据内容请参见该类型说明
        /// </summary>
        public Dictionary<string, BandWidthData> Data { get; set; }
    }

    /// <summary>
    /// 带宽-数据内容
    /// </summary>
    public class BandWidthData
    {
        /// <summary>
        /// 国内带宽数据
        /// </summary>
        public List<UInt64> China { get; set; }

        /// <summary>
        /// 海外带宽数据
        /// </summary>
        public List<UInt64> Oversea { get; set; }
    }
}
