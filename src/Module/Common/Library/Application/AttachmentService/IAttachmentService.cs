using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nm.Lib.Utils.Core.Result;
using Nm.Module.Common.Application.AttachmentService.ViewModels;
using Nm.Module.Common.Domain.Attachment.Models;

namespace Nm.Module.Common.Application.AttachmentService
{
    /// <summary>
    /// 附件表服务
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(AttachmentQueryModel model);

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="model"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResultModel> Upload(AttachmentUploadModel model, HttpRequest request);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel<FileDownloadModel>> Download(Guid id);

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel<FileDownloadModel>> Export(Guid id);
    }
}
