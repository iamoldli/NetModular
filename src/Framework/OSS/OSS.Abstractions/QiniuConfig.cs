using System.ComponentModel;

namespace NetModular.Lib.OSS.Abstractions
{
    public class QiniuConfig
    {
        /// <summary>
        /// 访问Key
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// 空间
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// 存储区域
        /// </summary>
        public QiniuZone Zone { get; set; }

        /// <summary>
        /// 令牌有效期
        /// </summary>
        public int TokenExpires { get; set; } = 7200;

        public bool Check()
        {
            return AccessKey.NotNull() && SecretKey.NotNull() && Domain.NotNull();
        }
    }

    /// <summary>
    /// 七牛存储区域
    /// </summary>
    public enum QiniuZone
    {
        /// <summary>
        /// 华 东
        /// </summary>
        [Description("华 东")]
        ZONE_CN_East,
        /// <summary>
        /// 华 北
        /// </summary>
        [Description("华 北")]
        ZONE_CN_North,
        /// <summary>
        /// 华 南
        /// </summary>
        [Description("华 南")]
        ZONE_CN_South,
        /// <summary>
        /// 北 美
        /// </summary>
        [Description("北 美")]
        ZONE_US_North
    }
}
