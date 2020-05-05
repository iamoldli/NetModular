namespace Qiniu.Storage
{
    /// <summary>
    /// 分片上传进度处理
    /// </summary>
    /// <param name="uploadedBytes">已上传的字节数</param>
    /// <param name="totalBytes">文件总字节数</param>
    public delegate void UploadProgressHandler(long uploadedBytes, long totalBytes);
}
