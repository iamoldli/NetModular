using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;

namespace NetModular.Module.Admin.Domain.FileOwner
{
    public interface IFileOwnerRepository : IRepository<FileOwnerEntity>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exist(FileOwnerEntity entity);
    }
}
