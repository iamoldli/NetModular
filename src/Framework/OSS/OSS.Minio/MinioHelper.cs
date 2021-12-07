using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Minio;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NetModular.Lib.OSS.Minio
{
    /// <summary>
    /// http://docs.minio.org.cn/docs/master/minio-monitoring-guide
    /// Minio 是一个非常轻量好用的私有文件服务，部署方便，文档齐全
    /// </summary>
    [Singleton]
    public class MinioHelper
    {
        private const int _maxExpireInt = 7 * 24 * 3600;
        private readonly ILogger<MinioHelper> _logger;
        private readonly MinioConfig _config;
        public MinioHelper(OSSConfig config, ILogger<MinioHelper> logger)
        {
            _logger = logger;
            _config = config.Minio;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>

        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string objectName, Stream data, CancellationToken cancellationToken = default, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            string contentType = null;
            if (data is FileStream fileStream)
            {
                string fileName = fileStream.Name;
                if (!string.IsNullOrEmpty(fileName))
                {
                    new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
                }
            }
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                await client.PutObjectAsync(bucketName, objectName, data, data.Length, contentType, null, null, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS文件上传异常：{@ex}", ex);
            }
            return false;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="objectName">objectName存在，自动替换为新的</param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string objectName, string filePath, CancellationToken cancellationToken = default, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            if (!File.Exists(filePath))
            {
                throw new Exception("File not exist.");
            }
            string fileName = Path.GetFileName(filePath);
            string contentType = null;
            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                await client.PutObjectAsync(bucketName, objectName, filePath, contentType, null, null, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS文件上传异常：{@ex}", ex);
            }
            return false;
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string objectName, Action<Stream> callback, CancellationToken cancellationToken = default, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                await client.GetObjectAsync(bucketName, objectName, (stream) =>
                {
                    callback(stream);
                }, null, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS获取文件异常：{@ex}", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string objectName, string filePath, CancellationToken cancellationToken = default, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            string dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                await client.GetObjectAsync(bucketName, objectName, filePath, null, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS获取文件异常：{@ex}", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件临时url
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="expiresInt"></param>
        /// <param name="accessMode"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<string> PresignedGetObjectAsync(string objectName, FileAccessMode accessMode = FileAccessMode.Open, int expiresInt = 0, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            if (expiresInt <= 0 || expiresInt > _maxExpireInt)
            {
                expiresInt = accessMode == FileAccessMode.Open ? _maxExpireInt : _config.ExpireInt;
            }
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                string presignedUrl = await client.PresignedGetObjectAsync(bucketName, objectName, expiresInt);
                return presignedUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS获取URL异常：{@ex}", ex);
                throw new Exception($"Presigned get url for {(accessMode == FileAccessMode.Open ? "open" : "private")} object '{objectName}' from {bucketName} failed. {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<bool> RemoveObjectAsync(string objectName, string bucketName = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            try
            {
                // 创建OssClient实例。
                var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
                await client.RemoveObjectAsync(bucketName, objectName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS文件删除异常：{@ex}", ex);
            }
            return false;
        }

        private void CheckParams(string bucketName, string objectName)
        {
            Check.NotNull(bucketName, nameof(bucketName));
            if (bucketName.Length < 3)
            {
                throw new ArgumentException("MinIO BucketName长度不得小于3位");
            }
            Check.NotNull(objectName, nameof(objectName));
        }
    }
}
