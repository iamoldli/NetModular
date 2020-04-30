using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.File.Models;

namespace NetModular.Module.Admin.Domain.File
{
    /// <summary>
    /// 文件仓储
    /// </summary>
    public interface IFileRepository : IRepository<FileEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<FileEntity>> Query(FileQueryModel model);

        /// <summary>
        /// 根据存储ID查询
        /// </summary>
        /// <param name="saveId"></param>
        /// <returns></returns>
        Task<FileEntity> GetBySaveId(string saveId);
    }
}
