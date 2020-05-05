using System.Collections.Generic;

namespace Qiniu.CDN
{
    /// <summary>
    /// 日志-消息内容结构
    /// 
    /// 以下是一个返回结果示例
    /// 
    /// {
    ///     "data": {
    ///             "log-test1.SOME_TEST.com": [
    ///                 {
    ///                     "name":"log-test1.SOME_TEST.com_2016-07-01-00_00.gz",
    ///                     "size": 88490306,
    ///                     "mtime": 1466274440,
    ///                     "url": "http://FUSION_LOG_DOWNLOAD_URL1"
    ///                  }
    ///              ],
    ///              "log-test2.SOME_TEST.com": [
    ///                 {
    ///                     "name":"log-test2.SOME_TEST.com_2016-07-01-00_00.gz",
    ///                     "size": 73280873,
    ///                     "mtime": 1466273259,
    ///                     "url": "http://FUSION_LOG_DOWNLOAD_URL2"
    ///                 }
    ///             ]
    ///         }
    /// }
    /// 
    /// /// </summary>
    public class LogListInfo
    {
        /// <summary>
        /// 代码 含义 说明
        /// 200	success	成功(OK)
        /// 400	invalid params	请求参数格式错误
        /// 401	bad token   认证授权失败（包括密钥信息不正确；数字签名错误；授权已超时）
        /// 400032	invalid host    请求中存在无效的域名，请确保域名格式正确
        /// 500000	internal error 服务器内部错误
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 错误消息(状态码非OK时)
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// 日志信息(与域名列表对应)
        /// </summary>
        public Dictionary<string,List<LogData>> Data { get; set; }
    }

    /// <summary>
    /// 日志信息内容
    /// </summary>
    public class LogData
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件大小，单位为 Byte
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件修改时间，Unix 时间戳
        /// </summary>
        public long Mtime { get; set; }

        /// <summary>
        /// 日志下载链接
        /// </summary>
        public string Url { get; set; }
    }
}
