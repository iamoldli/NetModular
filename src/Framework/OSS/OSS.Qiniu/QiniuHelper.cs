using System;
using System.IO;
using Microsoft.Extensions.Logging;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using Qiniu.Storage;
using Qiniu.Util;

namespace NetModular.Lib.OSS.Qiniu
{
    /// <summary>
    /// 七牛帮助类
    /// </summary>
    [Singleton]
    public class QiniuHelper
    {
        private readonly QiniuTokenManager _tokenManager;
        private readonly ILogger<QiniuHelper> _logger;
        private readonly QiniuConfig _config;

        public QiniuHelper(QiniuTokenManager tokenManager, ILogger<QiniuHelper> logger, OSSConfig config)
        {
            _tokenManager = tokenManager;
            _logger = logger;
            _config = config.Qiniu;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="physicalPath">物理路径</param>
        /// <param name="key">文件key</param>
        /// <returns></returns>
        public bool Upload(string physicalPath, string key)
        {
            if (physicalPath.IsNull())
                return false;

            var config = new Config
            {
                Zone = QiniuZone2Zone(_config.Zone),
                UseHttps = true,
                UseCdnDomains = true,
                ChunkSize = ChunkUnit.U512K
            };

            if (key.IsNull())
            {
                key = Path.GetFileName(physicalPath);
            }

            key = key.Replace('\\', '/');
            var target = new FormUploader(config);
            var result = target.UploadFile(physicalPath, key, _tokenManager.GetToken(), null);

            _logger.LogDebug("七牛上传结果：{@result}", result.ToString());

            return result.Code == 200;
        }

        /// <summary>
        /// 删除文件的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            if (key.IsNull())
                return false;

            var mac = new Mac(_config.AccessKey, _config.SecretKey);
            var config = new Config
            {
                Zone = QiniuZone2Zone(_config.Zone)
            };

            var bucketManager = new BucketManager(mac, config);
            key = key.Replace('\\', '/');

            var result = bucketManager.DeleteAfterDays(_config.Bucket, key, 7);
            _logger.LogDebug("七牛删除结果：{@result}", result.ToString());
            return result.Code == 200;
        }

        private Zone QiniuZone2Zone(QiniuZone zone)
        {
            switch (zone)
            {
                case QiniuZone.ZONE_CN_North:
                    return Zone.ZONE_CN_North;
                case QiniuZone.ZONE_CN_East:
                    return Zone.ZONE_CN_East;
                case QiniuZone.ZONE_CN_South:
                    return Zone.ZONE_CN_South;
                case QiniuZone.ZONE_US_North:
                    return Zone.ZONE_US_North;
            }

            throw new ArgumentException("七牛存储区域无效");
        }
    }
}
