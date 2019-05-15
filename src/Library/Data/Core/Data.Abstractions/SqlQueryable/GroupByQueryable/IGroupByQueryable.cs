using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable
{
    public interface IGroupByQueryable
    {
        #region ==ToList==

        /// <summary>
        /// 查询列表，返回匿名对象
        /// </summary>
        /// <returns></returns>
        IList<dynamic> ToList();

        /// <summary>
        /// 查询列表，返回指定对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IList<TResult> ToList<TResult>();

        /// <summary>
        /// 查询列表，返回匿名
        /// </summary>
        /// <returns></returns>
        Task<IList<dynamic>> ToListAsync();

        /// <summary>
        /// 查询列表，返回指定对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<IList<TResult>> ToListAsync<TResult>();

        #endregion

        #region ==First==

        /// <summary>
        /// 查询第一条数据，返回匿名对象
        /// </summary>
        /// <returns></returns>
        dynamic First();

        /// <summary>
        /// 查询第一条数据，返回指定类型
        /// </summary>
        /// <returns></returns>
        TResult First<TResult>();

        /// <summary>
        /// 查询第一条数据，返回匿名对象
        /// </summary>
        /// <returns></returns>
        Task<dynamic> FirstAsync();

        /// <summary>
        /// 查询第一条数据，返回指定类型
        /// </summary>
        /// <returns></returns>
        Task<TResult> FirstAsync<TResult>();

        #endregion
    }
}
