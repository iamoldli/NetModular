using System;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Lib.OSS.Aliyun
{
    public class AliyunFileStorageProvider : IFileStorageProvider
    {
        private readonly AliyunHelper _helper;
        private readonly AliyunConfig _config;
        public AliyunFileStorageProvider(AliyunHelper helper, OSSConfig config)
        {
            _helper = helper;
            _config = config.Aliyun;
        }

        public ValueTask<bool> Upload(FileObject fileObject)
        {
            var result = _helper.Upload(fileObject.PhysicalPath, fileObject.FileInfo.FullPath, fileObject.AccessMode);
            if (result)
            {
                fileObject.FileInfo.Url = GetUrl(fileObject.FileInfo.FullPath, fileObject.AccessMode);
                return new ValueTask<bool>(true);
            }
            return new ValueTask<bool>(false);
        }

        public ValueTask<bool> Delete(FileObject fileObject)
        {
            var result = _helper.Delete(fileObject.FileInfo.FullPath);
            return new ValueTask<bool>(result);
        }

        public string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open)
        {
            if (fullPath.IsNull())
                return string.Empty;
            if (fullPath.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || fullPath.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                return fullPath;

            fullPath = fullPath.Replace("\\", "/");

            //公开
            if (accessMode == FileAccessMode.Open)
            {
                return $"{_config.Domain}{fullPath}";
            }

            //私有
            try
            {
                // 创建OSSClient实例。
                var client = new OssClient(_config.Endpoint, _config.AccessKeyId, _config.AccessKeySecret);
                // 生成签名URL
                var req = new GeneratePresignedUriRequest(_config.BucketName, fullPath, SignHttpMethod.Get)
                {
                    Expiration = DateTime.Now.AddHours(4)
                };
                var uri = client.GeneratePresignedUri(req);

                return $"{_config.Domain}{uri.PathAndQuery}";
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }

            return string.Empty;
        }
    }
}
