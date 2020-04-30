using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Enums;

namespace NetModular.Lib.OSS.Abstractions
{
    /// <summary>
    /// 文件存储提供器
    /// </summary>
    public interface IFileStorageProvider
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        ValueTask<bool> Upload(FileObject fileObject);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileObject"></param>
        /// <returns></returns>
        ValueTask<bool> Delete(FileObject fileObject);

        /// <summary>
        /// 获取完整的访问URL
        /// </summary>
        /// <param name="fullPath">文件完整路径</param>
        /// <param name="accessMode">访问方式</param>
        /// <returns></returns>
        string GetUrl(string fullPath, FileAccessMode accessMode = FileAccessMode.Open);
    }
}
