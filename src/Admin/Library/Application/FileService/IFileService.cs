using System;
using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Files;
using NetModular.Module.Admin.Application.FileService.ViewModels;
using NetModular.Module.Admin.Domain.File;
using NetModular.Module.Admin.Domain.File.Models;

namespace NetModular.Module.Admin.Application.FileService
{
    /// <summary>
    /// 文件服务
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IResultModel> Query(FileQueryModel model);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        Task<IResultModel> Add(FileUploadModel model, FileInfo fileInfo);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> Delete(int id);

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResultModel> HardDelete(int id);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        Task<IResultModel<FileEntity>> Get(int id);

        /// <summary>
        /// 根据完整路径获取实体
        /// </summary>
        /// <param name="fullPath">完整路径</param>
        /// <returns></returns>
        Task<IResultModel> Get(string fullPath);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="fullPath">文件路径</param>
        /// <param name="accountId">账户编号</param>
        /// <returns></returns>
        Task<IResultModel<FileEntity>> Download(string fullPath, Guid accountId);
    }
}
