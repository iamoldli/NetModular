using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Enums;
using System;
using System.Collections.Generic;
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
        private const string _defaultMime = "application/octet-stream";
        private readonly MinioConfig _config;
        private readonly ILogger<MinioHelper> _logger;
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
        /// <param name="contentType"></param>
        /// <param name="metaData"></param>
        /// <param name="sse"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string objectName, Stream data, CancellationToken cancellationToken = default, string bucketName = default, string contentType = default, Dictionary<string, string> metaData = default, ServerSideEncryption sse = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            if (contentType.IsNull())
            {
                var ctp = new FileExtensionContentTypeProvider();
                if (data is not FileStream fileStream || fileStream.Name.IsNull() || !ctp.TryGetContentType(fileStream.Name, out contentType))
                {
                    if (!ctp.TryGetContentType(objectName, out contentType))
                    {
                        contentType = _defaultMime;
                    }
                }
            }
            try
            {
                var client = GetClient();
                await client.PutObjectAsync(bucketName, objectName, data, data.Length, contentType, metaData, sse, cancellationToken);
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
        /// <param name="contentType"></param>
        /// <param name="metaData"></param>
        /// <param name="sse"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string objectName, string filePath, CancellationToken cancellationToken = default, string bucketName = default, string contentType = default, Dictionary<string, string> metaData = default, ServerSideEncryption sse = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not exist.");
            }
            if (contentType.IsNull())
            {
                var ctp = new FileExtensionContentTypeProvider();
                if (!ctp.TryGetContentType(Path.GetFileName(filePath), out contentType))
                {
                    if (!ctp.TryGetContentType(objectName, out contentType))
                    {
                        contentType = _defaultMime;
                    }
                }
            }
            try
            {
                var client = GetClient();
                await client.PutObjectAsync(bucketName, objectName, filePath, contentType, metaData, sse, cancellationToken);
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
        /// <param name="sse"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string objectName, Action<Stream> callback, CancellationToken cancellationToken = default, string bucketName = default, ServerSideEncryption sse = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            try
            {
                var client = GetClient();
                await client.GetObjectAsync(bucketName, objectName, (stream) =>
                {
                    callback?.Invoke(stream);
                }, sse, cancellationToken);
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
        /// <param name="sse"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string objectName, string filePath, CancellationToken cancellationToken = default, string bucketName = default, ServerSideEncryption sse = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            var dir = Path.GetDirectoryName(filePath);
            if (dir.NotNull() && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            try
            {
                var client = GetClient();
                await client.GetObjectAsync(bucketName, objectName, filePath, sse, cancellationToken);
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
        /// <param name="reqParams"></param>
        /// <param name="reqDate"></param>
        /// <returns></returns>
        public async Task<string> PresignedGetObjectAsync(string objectName, FileAccessMode accessMode = FileAccessMode.Open, int expiresInt = 0, string bucketName = default, Dictionary<string, string> reqParams = default, DateTime? reqDate = default)
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
                var client = GetClient();
                var presignedUrl = await client.PresignedGetObjectAsync(bucketName, objectName, expiresInt, reqParams, reqDate);
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
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> RemoveObjectAsync(string objectName, string bucketName = default, CancellationToken cancellationToken = default)
        {
            if (bucketName.IsNull())
            {
                bucketName = _config.BucketName;
            }
            CheckParams(bucketName, objectName);
            try
            {
                var client = GetClient();
                await client.RemoveObjectAsync(bucketName, objectName, cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("MinIO OSS文件删除异常：{@ex}", ex);
            }
            return false;
        }

        private MinioClient GetClient()
        {
            var client = new MinioClient(_config.EndPoint, _config.AccessKey, _config.SecretKey);
            if (_config.Secure)
            {
                client.WithSSL();
            }

            return client;
        }

        private void CheckParams(string bucketName, string objectName)
        {
            Check.NotNull(bucketName, nameof(bucketName));
            if (bucketName.Length < 3)
            {
                throw new ArgumentOutOfRangeException("MinIO BucketName长度不得小于3位");
            }
            Check.NotNull(objectName, nameof(objectName));
        }
    }
}
