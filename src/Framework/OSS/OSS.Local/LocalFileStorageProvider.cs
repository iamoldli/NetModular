using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetModular.Lib.Config.Abstractions;
using NetModular.Lib.Host.Web.Options;
using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using NetModular.Lib.Utils.Mvc.Extensions;

namespace NetModular.Lib.OSS.Local
{
    /// <summary>
    /// 本地文件存储提供器
    /// </summary>
    public class LocalFileStorageProvider : IFileStorageProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigProvider _configProvider;

        public LocalFileStorageProvider(IHttpContextAccessor httpContextAccessor, IConfigProvider configProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _configProvider = configProvider;
        }

        public ValueTask<bool> Upload(FileObject fileObject)
        {
            fileObject.FileInfo.Url = GetUrl(fileObject.FileInfo.FullPath, fileObject.AccessMode);
            return new ValueTask<bool>(true);
        }

        public ValueTask<bool> Delete(FileObject fileObject)
        {
            if (fileObject == null || fileObject.FileInfo == null || fileObject.FileInfo.FullPath.IsNull())
                return new ValueTask<bool>(false);

            var config = _configProvider.Get<Lib.Config.Abstractions.Impl.PathConfig>();
            var path = Path.Combine(config.UploadPath, "Admin", "OSS", fileObject.AccessMode == FileAccessMode.Open ? "Open" : "Private", fileObject.FileInfo.FullPath);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            return new ValueTask<bool>(true);
        }

        public string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open)
        {
            if (fullPath.IsNull())
                return string.Empty;
            if (fullPath.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || fullPath.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                return fullPath;

            var request = _httpContextAccessor.HttpContext.Request;
            //p表示私有的文件private，o表示公开的文件open
            var path = $"/oss/{(accessMode == FileAccessMode.Open ? "o" : "p")}/{fullPath}";

            return new Uri(request.GetHost(path)).ToString();
        }
    }
}
