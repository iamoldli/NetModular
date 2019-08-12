using System;
using System.Threading.Tasks;
using Tm.Lib.Utils.Core.Files;
using Tm.Lib.Utils.Core.Result;
using Tm.Module.Common.Application.AttachmentService.ResultModels;
using Tm.Module.Common.Application.AttachmentService.ViewModels;
using Tm.Module.Common.Domain.Attachment.Models;

namespace Tm.Module.Common.Application.AttachmentService
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
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        Task<IResultModel<AttachmentUploadResultModel>> Upload(AttachmentUploadModel model, FileInfo fileInfo);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="id"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IResultModel<FileDownloadModel>> Download(Guid id, Guid accountId);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        Task<IResultModel> Delete(Guid id);
    }
}
