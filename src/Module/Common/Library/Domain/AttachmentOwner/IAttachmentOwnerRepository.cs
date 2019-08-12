using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;

namespace Tm.Module.Common.Domain.AttachmentOwner
{
    public interface IAttachmentOwnerRepository : IRepository<AttachmentOwnerEntity>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exist(AttachmentOwnerEntity entity);
    }
}
