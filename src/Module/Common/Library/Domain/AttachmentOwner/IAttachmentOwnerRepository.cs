using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.Common.Domain.AttachmentOwner
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
