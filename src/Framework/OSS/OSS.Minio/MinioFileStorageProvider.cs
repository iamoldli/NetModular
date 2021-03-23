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
        private readonly MinioConfig _minoConfig;
        public MinioFileStorageProvider(MinioHelper helper, OSSConfig config)
        {
            _helper = helper;
            _minoConfig = config.Minio;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public async ValueTask<bool> Delete(FileObject fileObject)
        {
            await _helper.RemoveObjectAsync(_minoConfig.Bucketname, fileObject.FileInfo.SaveName);
            return await new ValueTask<bool>(true);
        }

        /// <summary>
        /// 获取文件Url
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="accessMode"></param>
        /// <returns></returns>
        public string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open)
        {
            var filename = Path.GetFileName(fullPath);
            var url= _helper.PresignedGetObjectAsync(_minoConfig.Bucketname, filename,1000).Result;
            return url;
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        public async ValueTask<bool> Upload(FileObject fileObject)
        {
            await _helper.PutObjectAsync(_minoConfig.Bucketname, fileObject.FileInfo.SaveName, fileObject.PhysicalPath);
            return await new ValueTask<bool>(true);
        }
    }
}
