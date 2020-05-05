using System.Collections.Generic;

namespace Qiniu.Storage
{
    /// <summary>
    /// 文件上传的额外可选设置
    /// </summary>
    public class PutExtra
    {
        /// <summary>
        /// 设置文件断点续传进度记录文件
        /// </summary>
        public string ResumeRecordFile { set; get; }
        /// <summary>
        /// 上传可选参数字典，参数名次以 x: 开头
        /// </summary>
        public Dictionary<string, string> Params;
        /// <summary>
        /// 指定文件的MimeType
        /// </summary>
        public string MimeType { set; get; }
        /// <summary>
        /// 设置文件上传进度处理器
        /// </summary>
        public UploadProgressHandler ProgressHandler { set; get; }
        /// <summary>
        /// 设置文件上传的状态控制器
        /// </summary>
        public UploadController UploadController { set; get; }
        
        /// <summary>
        /// 最大重试次数
        /// </summary>
        public int MaxRetryTimes { set; get; }
    }
}
