using System;
using System.Threading.Tasks;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using Qiniu.Storage;
using Qiniu.Util;

namespace NetModular.Lib.OSS.Qiniu
{
    public class QiniuFileStorageProvider : IFileStorageProvider
    {
        private readonly QiniuHelper _helper;
        private readonly QiniuConfig _config;
        public QiniuFileStorageProvider(QiniuHelper helper, OSSConfig config)
        {
            _helper = helper;
            _config = config.Qiniu;
        }

        public ValueTask<bool> Upload(FileObject fileObject)
        {
            var result = _helper.Upload(fileObject.PhysicalPath, fileObject.FileInfo.FullPath);
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

            fullPath = fullPath.Replace('\\', '/');

            //公开
            if (accessMode == FileAccessMode.Open)
            {
                return DownloadManager.CreatePublishUrl(_config.Domain, fullPath);
            }

            //私有
            var mac = new Mac(_config.AccessKey, _config.SecretKey);
            return DownloadManager.CreatePrivateUrl(mac, _config.Domain, fullPath);
        }
    }
}
