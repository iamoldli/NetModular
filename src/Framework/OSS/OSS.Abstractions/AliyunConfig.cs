namespace NetModular.Lib.OSS.Abstractions
{
    public class AliyunConfig
    {
        /// <summary>
        /// 域名
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 访问令牌ID
        /// </summary>
        public string AccessKeyId { get; set; }

        /// <summary>
        /// 访问令牌密钥
        /// </summary>
        public string AccessKeySecret { get; set; }

        /// <summary>
        /// 存储空间名称
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 自定义域名
        /// </summary>
        public string Domain { get; set; }
    }
}
