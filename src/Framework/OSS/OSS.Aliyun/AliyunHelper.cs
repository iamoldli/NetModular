using System;
using Aliyun.OSS;
using Microsoft.Extensions.Logging;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Lib.OSS.Aliyun
{
    /// <summary>
    /// 阿里云帮助类
    /// </summary>
    [Singleton]
    public class AliyunHelper
    {
        private readonly ILogger<AliyunHelper> _logger;
        private readonly AliyunConfig _config;

        public AliyunHelper(OSSConfig config, ILogger<AliyunHelper> logger)
        {
            _logger = logger;
            _config = config.Aliyun;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="physicalPath">物理路径</param>
        /// <param name="key">文件key</param>
        /// <param name="accessMode">访问权限</param>
        /// <returns></returns>
        public bool Upload(string physicalPath, string key, FileAccessMode accessMode)
        {
            // 创建OssClient实例。
            var client = new OssClient(_config.Endpoint, _config.AccessKeyId, _config.AccessKeySecret);
            try
            {
                // 上传文件。
                client.PutObject(_config.BucketName, key, physicalPath);

                client.SetObjectAcl(_config.BucketName, key, accessMode == FileAccessMode.Open ? CannedAccessControlList.PublicRead : CannedAccessControlList.Private);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("阿里云OSS文件上传异常：{@ex}", ex);
            }

            return false;
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

            // 创建OssClient实例。
            var client = new OssClient(_config.Endpoint, _config.AccessKeyId, _config.AccessKeySecret);
            try
            {
                // 删除文件
                client.DeleteObject(_config.BucketName, key);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("阿里云OSS文件删除异常：{@ex}", ex);
            }
            return false;
        }
    }
}
