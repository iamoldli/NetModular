using System.Threading.Tasks;
using Data.Common.Domain;
using NetModular.Lib.Data.Abstractions;

namespace Data.Common.Repository
{
    public interface IArticleRepository : IRepository<ArticleEntity>
    {
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        Task<long> Count();

        /// <summary>
        /// 增加数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<bool> AddCount(int id, int count);
    }
}
