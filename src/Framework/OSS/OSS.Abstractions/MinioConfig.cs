namespace NetModular.Lib.OSS.Abstractions
{
    /// <summary>
    /// Mino使用： http://docs.minio.org.cn/docs/master/minio-monitoring-guide
    /// </summary>
    public class MinioConfig
    {
        /// <summary>
        /// 对象存储服务的URL：localhost:9000
        /// </summary>
        public string EndPoint { get; set; }

        /// <summary>
        /// Access key是唯一标识你的账户的用户ID。
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Secret key是你账户的密码。
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// true代表使用HTTPS。
        /// </summary>
        public bool Secure { get; set; }

        /// <summary>
        /// 存储桶
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 临时过期秒
        /// 最大值7*24*3600
        /// </summary>
        public int ExpireInt { get; set; } = 3600;
    }
}
