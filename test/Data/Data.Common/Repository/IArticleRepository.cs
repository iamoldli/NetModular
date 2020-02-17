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
    }
}
