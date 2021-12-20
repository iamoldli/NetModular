using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Encrypt;
using NetModular.Lib.Utils.Core.Files;
using FileInfo = NetModular.Lib.Utils.Core.Files.FileInfo;

namespace NetModular.Lib.Utils.Mvc.Helpers
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    [Singleton]
    public class FileUploadHelper
    {
        /// <summary>
        /// 单文件文件上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResultModel<FileInfo>> Upload(FileUploadModel model, CancellationToken cancellationToken = default)
        {
            var result = new ResultModel<FileInfo>();

            if (model.FormFile == null || model.FormFile.Length < 1)
            {
                if (model.Request.Form.Files != null && model.Request.Form.Files.Any())
                {
                    model.FormFile = model.Request.Form.Files[0];
                }
            }

            if (model.FormFile == null || model.FormFile.Length < 1)
                return result.Failed("请选择文件!");

            var name = model.FileName.IsNull() ? model.FormFile.FileName : model.FileName;
            var size = model.FormFile.Length;
            var fileInfo = new FileInfo(name, size);

            if (model.MaxSize > 0 && model.MaxSize < size)
                return result.Failed($"文件大小不能超过{new FileSize(model.MaxSize).ToString()}");

            if (model.LimitExt != null && !model.LimitExt.Any(m => m.EqualsIgnoreCase(fileInfo.Ext)))
                return result.Failed($"文件格式无效，请上传{model.LimitExt.Aggregate((x, y) => x + "," + y)}格式的文件");

            var date = DateTime.Now;
            fileInfo.Path = Path.Combine(model.RelativePath, date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"));
            var resultModel = await UploadSave(model.FormFile, fileInfo, model.RootPath, model.CalcMd5, cancellationToken);
            return result.Success(resultModel);
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IResultModel<IList<FileInfo>>> Upload(FileUploadMultipleModel model, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("多文件上传暂未实现");

            //var result = new ResultModel<IList<FileInfo>>();

            //if (model.FormFiles == null || !model.FormFiles.Any())
            //{
            //    if (model.Request.Form.Files != null && model.Request.Form.Files.Any())
            //    {
            //        model.FormFiles = model.Request.Form.Files.ToList();
            //    }
            //}

            //if (model.FormFiles == null || !model.FormFiles.Any())
            //    return result.Failed("请选择文件!");

            //var tasks = new List<Task<FileInfo>>();
            //foreach (var formFile in model.FormFiles)
            //{
            //    tasks.Add(UploadSave(formFile, model.RelativePath, model.RootPath, cancellationToken));
            //}

            //var list = await Task.WhenAll(tasks);

            //return result.Success(list);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="rootPath">根目录</param>
        /// <param name="calcMd5"></param>
        /// <param name="cancellationToken">取消token</param>
        /// <returns></returns>
        private async Task<FileInfo> UploadSave(IFormFile formFile, FileInfo fileInfo, string rootPath, bool calcMd5, CancellationToken cancellationToken = default)
        {
            fileInfo.SaveName = $"{Guid.NewGuid().ToString().Replace("-", "")}.{fileInfo.Ext}";

            var fullDir = Path.Combine(rootPath, fileInfo.Path);
            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            //写入
            var fullPath = Path.Combine(fullDir, fileInfo.SaveName);

            if (calcMd5)
                fileInfo.Md5 = await SaveWithMd5(formFile, fullPath, cancellationToken);
            else
                await Save(formFile, fullPath, cancellationToken);

            return fileInfo;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="savePath"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Save(IFormFile formFile, string savePath, CancellationToken cancellationToken = default)
        {
            //写入
            using var stream = new FileStream(savePath, FileMode.Create);
            return formFile.CopyToAsync(stream, cancellationToken);
        }

        /// <summary>
        /// 保存文件，返回文件的MD5值
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public async Task<string> SaveWithMd5(IFormFile formFile, string savePath, CancellationToken cancellationToken = default)
        {
            await using var stream = new FileStream(savePath, FileMode.Create);
            await formFile.CopyToAsync(stream, cancellationToken);
            stream.Seek(0, SeekOrigin.Begin);
            return Md5Encrypt.Encrypt(stream);
        }

        [Obsolete("弃用方法，请及时更替为SaveWithMd5")]
        public Task<string> SaveWidthMd5(IFormFile formFile, string savePath, CancellationToken cancellationToken = default)
            => SaveWithMd5(formFile, savePath, cancellationToken);
    }

    /// <summary>
    /// 单文件上传
    /// </summary>
    public class FileUploadModel
    {
        /// <summary>
        /// 当前请求
        /// </summary>
        public HttpRequest Request { get; set; }

        /// <summary>
        /// 上传的文件对象
        /// </summary>
        public IFormFile FormFile { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 存储根路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string Module { get; set; } = string.Empty;

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; } = string.Empty;

        /// <summary>
        /// 路径
        /// </summary>
        public string SubPath { get; set; } = string.Empty;

        /// <summary>
        /// 最大允许大小(单位：字节，为0表示不限制)
        /// </summary>
        public long MaxSize { get; set; }

        /// <summary>
        /// 限制后缀名
        /// </summary>
        public List<string> LimitExt { get; set; }

        /// <summary>
        /// 计算文件的MD5
        /// </summary>
        public bool CalcMd5 { get; set; } = true;

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullPath => Path.Combine(RootPath, RelativePath);

        /// <summary>
        /// 相对目录
        /// </summary>
        public string RelativePath => Path.Combine(Module, Group, SubPath ?? String.Empty);
    }

    /// <summary>
    /// 多文件上传
    /// </summary>
    public class FileUploadMultipleModel
    {
        /// <summary>
        /// 当前请求
        /// </summary>
        public HttpRequest Request { get; set; }

        /// <summary>
        /// 上传的文件对象
        /// </summary>
        public IList<IFormFile> FormFiles { get; set; }

        /// <summary>
        /// 存储根路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullPath => Path.Combine(RootPath, Module, Group);

        /// <summary>
        /// 相对目录
        /// </summary>
        public string RelativePath => Path.Combine(Module, Group);
    }
}
