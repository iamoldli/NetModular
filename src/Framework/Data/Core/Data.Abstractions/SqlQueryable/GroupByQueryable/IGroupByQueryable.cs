using System.Collections.Generic;
using System.Data;
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

        #region ==ToReader==

        /// <summary>
        /// 查询列表，返回IDataReader
        /// </summary>
        /// <returns></returns>
        IDataReader ToReader();

        /// <summary>
        /// 查询列表，返回IDataReader
        /// </summary>
        /// <returns></returns>
        Task<IDataReader> ToReaderAsync();

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
