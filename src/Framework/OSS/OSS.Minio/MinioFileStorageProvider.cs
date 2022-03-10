using NetModular.Lib.OSS.Abstractions;
using NetModular.Lib.Utils.Core.Enums;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NetModular.Lib.OSS.Minio
{
    public class MinioFileStorageProvider : IFileStorageProvider
    {

        private readonly MinioHelper _helper;
        public MinioFileStorageProvider(MinioHelper helper)
        {
            _helper = helper;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public async ValueTask<bool> Upload(FileObject fileObject)
        {
            var result = await _helper.PutObjectAsync(fileObject.FileInfo.SaveName, fileObject.PhysicalPath);
            if (result)
            {
                fileObject.FileInfo.Url = GetUrl(fileObject.FileInfo.FullPath, fileObject.AccessMode);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public async ValueTask<bool> Delete(FileObject fileObject)
        {
            var result = await _helper.RemoveObjectAsync(fileObject.FileInfo.SaveName);
            return await new ValueTask<bool>(result);
        }

        /// <summary>
        /// 获取文件Url
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="accessMode"></param>
        /// <returns></returns>
        public string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open)
        {
            if (fullPath.IsNull())
            {
                return string.Empty;
            }
            if (fullPath.StartsWith("http:", StringComparison.OrdinalIgnoreCase) || fullPath.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
            {
                return fullPath;
            }

            var filename = Path.GetFileName(fullPath);
            var url = _helper.PresignedGetObjectAsync(filename, accessMode).Result;
            return url;
        }
    }
}
