using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions.Pagination;

namespace NetModular.Lib.Data.Abstractions.SqlQueryable
{
    public interface INetSqlQueryable
    {
        #region ==ToList==

        /// <summary>
        /// 查询列表，返回匿名对象
        /// </summary>
        /// <returns></returns>
        IList<dynamic> ToList();

        /// <summary>
        /// 查询列表，返回指定类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        IList<TResult> ToList<TResult>();

        /// <summary>
        /// 查询列表，返回匿名对象
        /// </summary>
        /// <returns></returns>
        Task<IList<dynamic>> ToListAsync();

        /// <summary>
        /// 查询列表，返回指定类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        Task<IList<TResult>> ToListAsync<TResult>();

        #endregion

        #region ==Pagination==

        /// <summary>
        /// 分页查询，返回匿名对象
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        IList<dynamic> Pagination(Paging paging = null);

        /// <summary>
        /// 分页查询，返回指定类型
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        IList<TResult> Pagination<TResult>(Paging paging = null);

        /// <summary>
        /// 分页查询，返回匿名对象
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<IList<dynamic>> PaginationAsync(Paging paging = null);

        /// <summary>
        /// 分页查询，返回指定类型
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        Task<IList<TResult>> PaginationAsync<TResult>(Paging paging = null);

        #endregion

        #region ==Count==

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <returns></returns>
        long Count();

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <returns></returns>
        Task<long> CountAsync();

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

        #region ==Exists==

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        Task<bool> ExistsAsync();

        #endregion

        #region ==获取Sql语句==

        /// <summary>
        /// 获取Sql语句
        /// </summary>
        /// <returns></returns>
        string ToSql();

        /// <summary>
        /// 获取Sql语句并返回参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        string ToSql(out IQueryParameters parameters);

        #endregion
    }
}
