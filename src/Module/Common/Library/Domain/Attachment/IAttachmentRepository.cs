using System.Collections.Generic;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.Common.Domain.Attachment.Models;

namespace Tm.Module.Common.Domain.Attachment
{
    /// <summary>
    /// 附件表仓储
    /// </summary>
    public interface IAttachmentRepository : IRepository<AttachmentEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<AttachmentEntity>> Query(AttachmentQueryModel model);
    }
}
