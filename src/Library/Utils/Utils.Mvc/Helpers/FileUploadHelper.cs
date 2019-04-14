using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetModular.Lib.Utils.Core.Attributes;
using NetModular.Lib.Utils.Core.Encrypt;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Lib.Utils.Mvc.Helpers
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    [Singleton]
    public class FileUploadHelper
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IResultModel<FileUploadResultModel>> Upload(FileUploadModel model, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = new ResultModel<FileUploadResultModel>();

            if (model.FormFile == null || model.FormFile.Length < 1)
            {
                if (model.Request.Form.Files != null && model.Request.Form.Files.Any())
                {
                    model.FormFile = model.Request.Form.Files[0];
                }
            }

            if (model.FormFile == null || model.FormFile.Length < 1)
                return result.Failed("请选择文件!");

            var resultModel = new FileUploadResultModel();
            var date = DateTime.Now;

            resultModel.Name = model.FormFile.FileName;
            resultModel.Ext = Path.GetExtension(model.FormFile.FileName) ?? string.Empty;
            resultModel.Path = Path.Combine(model.Group, date.ToString("yyyyMMdd"));
            resultModel.FileName = $"{date:yyyyMMddHHmmssfff}{resultModel.Ext}";
            resultModel.Size = model.FormFile.Length;

            //删除后缀名的点
            if (resultModel.Ext.NotNull())
            {
                resultModel.Ext = resultModel.Ext.Replace(".", "");
            }

            var fullDir = Path.Combine(model.RootPath, resultModel.Path);
            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            resultModel.Path = Path.Combine(resultModel.Path, resultModel.FileName);

            //写入
            var fullPath = Path.Combine(fullDir, resultModel.FileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                resultModel.Md5 = Md5Encrypt.Encrypt(stream);
                await model.FormFile.CopyToAsync(stream, cancellationToken);
            }

            return result.Success(resultModel);
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
        /// 分组
        /// </summary>
        public string Group { get; set; }
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
        public List<IFormFile> FormFiles { get; set; }

        /// <summary>
        /// 存储根路径
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; }
    }

    /// <summary>
    /// 文件上传返回模型
    /// </summary>
    public struct FileUploadResultModel
    {
        /// <summary>
        /// 原始文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 存储文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 文件的MD5值
        /// </summary>
        public string Md5 { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        public string Url { get; set; }
    }
}
