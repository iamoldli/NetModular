using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.OSS.Minio.Models;
using NetModular.Lib.Utils.Core.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetModular.Lib.OSS.Minio
{
    /// <summary>
    /// http://docs.minio.org.cn/docs/master/minio-monitoring-guide
    /// Minio 是一个非常轻量好用的私有文件服务，部署方便，文档齐全
    /// </summary>
    [Singleton]
    public  class MinioHelper
    {
        private static MinioClient _client = null;
        public MinioHelper(OSSConfig config)
        {
            _client = new MinioClient(config.Minio.Endpoint,
                config.Minio.AccessKey,
                config.Minio.SecretKey
                );
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="data"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string bucketName, string objectName, Stream data, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException(nameof(bucketName));
            }
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }
            string contentType = "application/octet-stream";
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
            await _client.PutObjectAsync(bucketName, objectName, data, data.Length, contentType, null, null, cancellationToken);
            return true;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName">objectName存在，自动替换为新的</param>
        /// <param name="filePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string bucketName, string objectName, string filePath, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException(nameof(bucketName));
            }
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }
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
            await _client.PutObjectAsync(bucketName, objectName, filePath, contentType, null, null, cancellationToken);
            return true;
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="callback"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string bucketName, string objectName, Action<Stream> callback, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException(nameof(bucketName));
            }
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }
            await _client.GetObjectAsync(bucketName, objectName, (stream) =>
            {
                callback(stream);
            }, null, cancellationToken);
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="fileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task GetObjectAsync(string bucketName, string objectName, string fileName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException(nameof(bucketName));
            }
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }
            string path = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            await _client.GetObjectAsync(bucketName, objectName, fileName, null, cancellationToken);
        }

        public async Task<string> PresignedGetObjectAsync(string bucketName, string objectName, int expiresInt)
        {
            return await PresignedObjectAsync(bucketName, objectName, expiresInt, PresignedObjectType.Get);
        }

        /// <summary>
        /// 获取文件临时url
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <param name="expiresInt"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private async Task<string> PresignedObjectAsync(string bucketName, string objectName, int expiresInt, PresignedObjectType type)
        {
            try
            {
                if (string.IsNullOrEmpty(bucketName))
                {
                    throw new ArgumentNullException(nameof(bucketName));
                }
                if (string.IsNullOrEmpty(objectName))
                {
                    throw new ArgumentNullException(nameof(objectName));
                }
                if (expiresInt <= 0)
                {
                    throw new Exception("ExpiresIn time can not less than 0.");
                }
                if (expiresInt > 7 * 24 * 3600)
                {
                    throw new Exception("ExpiresIn time no more than 7 days.");
                }
                string presignedUrl = await _client.PresignedGetObjectAsync(bucketName, objectName, expiresInt);
                return presignedUrl;
            }
            catch (Exception ex)
            {
                throw new Exception($"Presigned {(type == PresignedObjectType.Get ? "get" : "put")} url for object '{objectName}' from {bucketName} failed. {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectName"></param>
        /// <returns></returns>
        public async Task<bool> RemoveObjectAsync(string bucketName, string objectName)
        {
            if (string.IsNullOrEmpty(bucketName))
            {
                throw new ArgumentNullException(nameof(bucketName));
            }
            if (string.IsNullOrEmpty(objectName))
            {
                throw new ArgumentNullException(nameof(objectName));
            }

            await _client.RemoveObjectAsync(bucketName, objectName);
            return true;
        }
    }
}
