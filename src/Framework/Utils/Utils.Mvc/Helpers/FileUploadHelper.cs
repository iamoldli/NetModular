using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Encrypt;
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

            var resultModel = await UploadSave(model.FormFile, model.RelativePath, model.RootPath, cancellationToken);

            return result.Success(resultModel);
        }

        /// <summary>
        /// 多文件上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResultModel<IList<FileInfo>>> Upload(FileUploadMultipleModel model, CancellationToken cancellationToken = default)
        {
            var result = new ResultModel<IList<FileInfo>>();

            if (model.FormFiles == null || !model.FormFiles.Any())
            {
                if (model.Request.Form.Files != null && model.Request.Form.Files.Any())
                {
                    model.FormFiles = model.Request.Form.Files.ToList();
                }
            }

            if (model.FormFiles == null || !model.FormFiles.Any())
                return result.Failed("请选择文件!");

            var tasks = new List<Task<FileInfo>>();
            foreach (var formFile in model.FormFiles)
            {
                tasks.Add(UploadSave(formFile, model.RelativePath, model.RootPath, cancellationToken));
            }

            var list = await Task.WhenAll(tasks);

            return result.Success(list);
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="formFile">文件</param>
        /// <param name="relativePath">相对目录</param>
        /// <param name="rootPath">根目录</param>
        /// <param name="cancellationToken">取消token</param>
        /// <returns></returns>
        private async Task<FileInfo> UploadSave(IFormFile formFile, string relativePath, string rootPath, CancellationToken cancellationToken = default)
        {
            var date = DateTime.Now;

            var name = formFile.FileName;
            var size = formFile.Length;
            var fileInfo = new FileInfo(name, size)
            {
                Path = Path.Combine(relativePath, date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd")),
            };

            fileInfo.SaveName = $"{Guid.NewGuid().ToString().Replace("-", "")}.{fileInfo.Ext}";

            var fullDir = Path.Combine(rootPath, fileInfo.Path);
            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            //写入
            var fullPath = Path.Combine(fullDir, fileInfo.SaveName);
            fileInfo.Md5 = await SaveWidthMd5(formFile, fullPath, cancellationToken);

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
        public async Task<string> SaveWidthMd5(IFormFile formFile, string savePath, CancellationToken cancellationToken = default)
        {
            //写入
            await using var stream = new FileStream(savePath, FileMode.Create);
            var md5 = Md5Encrypt.Encrypt(stream);
            await formFile.CopyToAsync(stream, cancellationToken);

            return md5;
        }
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
        /// 存储根路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 模块编码
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string SubPath { get; set; }

        /// <summary>
        /// 完整目录
        /// </summary>
        public string FullPath => Path.Combine(RootPath, RelativePath);

        /// <summary>
        /// 相对目录
        /// </summary>
        public string RelativePath => Path.Combine(Module, Group, SubPath);
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
