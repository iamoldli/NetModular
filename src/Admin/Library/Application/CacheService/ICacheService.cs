using System.Threading.Tasks;
using NetModular.Lib.Utils.Core.Result;

namespace NetModular.Module.Admin.Application.CacheService
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// 查询指定模块的所有缓存键信息
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        IResultModel Query(string moduleCode);

        /// <summary>
        /// 清除指定前缀的缓存
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        Task<IResultModel> Clear(string prefix);
    }
}
