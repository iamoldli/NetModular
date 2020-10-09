using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal abstract class GroupByQueryableAbstract : IGroupByQueryable
    {
        protected readonly IDbSet Db;
        protected readonly QueryBody QueryBody;
        protected readonly QueryBuilder QueryBuilder;

        protected GroupByQueryableAbstract(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression)
        {
            Db = db;
            QueryBody = queryBody;
            QueryBuilder = queryBuilder;
            QueryBody.SetGroupBy(expression);
        }

        protected void SetHaving(LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.Having.Add(expression);
        }

        public void SetOrderBy(string customOrderBy)
        {
            if (customOrderBy.NotNull())
                QueryBody.SetOrderBy(new Sort(customOrderBy));
        }

        public void SetOrderByDescending(string customOrderBy)
        {
            if (customOrderBy.NotNull())
                QueryBody.SetOrderBy(new Sort(customOrderBy, SortType.Desc));
        }

        public void SetOrderBy(LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.SetOrderBy(expression);
        }

        public void SetOrderByDescending(LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.SetOrderBy(expression, SortType.Desc);
        }

        public void SetSelect(LambdaExpression expression)
        {
            if (expression != null)
                QueryBody.Select = expression;
        }

        public void SetLimit(int skip, int take)
        {
            QueryBody.SetLimit(skip, take);
        }

        #region ==ToList==

        public IList<dynamic> ToList()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.Query(sql, parameters.Parse(), QueryBody.Uow).ToList();
        }

        public IList<TResult> ToList<TResult>()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.Query<TResult>(sql, parameters.Parse(), QueryBody.Uow).ToList();
        }

        public async Task<IList<dynamic>> ToListAsync()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync(sql, parameters.Parse(), QueryBody.Uow)).ToList();
        }

        public async Task<IList<TResult>> ToListAsync<TResult>()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return (await Db.QueryAsync<TResult>(sql, parameters.Parse(), QueryBody.Uow)).ToList();
        }

        #endregion

        #region ==Pagination==

        public IList<dynamic> Pagination(Paging paging = null, bool queryCount = true)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            if (queryCount)
                paging.TotalCount = Count();

            return ToList();
        }

        public IList<TResult> Pagination<TResult>(Paging paging = null, bool queryCount = true)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            if (queryCount)
                paging.TotalCount = Count();

            return ToList<TResult>();
        }

        public async Task<IList<dynamic>> PaginationAsync(Paging paging = null, bool queryCount = true)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            if (queryCount)
                paging.TotalCount = await CountAsync();

            return await ToListAsync();
        }

        public async Task<IList<TResult>> PaginationAsync<TResult>(Paging paging = null, bool queryCount = true)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            if (queryCount)
                paging.TotalCount = await CountAsync();

            return await ToListAsync<TResult>();
        }

        #endregion

        #region ==First==

        public dynamic First()
        {
            var sql = QueryBuilder.GroupByFirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault(sql, parameters.Parse(), QueryBody.Uow);
        }

        public TResult First<TResult>()
        {
            var sql = QueryBuilder.GroupByFirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefault<TResult>(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<dynamic> FirstAsync()
        {
            var sql = QueryBuilder.GroupByFirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<TResult> FirstAsync<TResult>()
        {
            var sql = QueryBuilder.GroupByFirstSqlBuild(out IQueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync<TResult>(sql, parameters.Parse(), QueryBody.Uow);
        }

        #endregion

        #region ==Count==

        public long Count()
        {
            var sql = QueryBuilder.CountSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalar<long>(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<long> CountAsync()
        {
            var sql = QueryBuilder.CountSqlBuild(out IQueryParameters parameters);
            return Db.ExecuteScalarAsync<long>(sql, parameters.Parse(), QueryBody.Uow);
        }

        #endregion

        #region ==ToReader==

        public IDataReader ToReader()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.ExecuteReader(sql, parameters.Parse(), QueryBody.Uow);
        }

        public Task<IDataReader> ToReaderAsync()
        {
            var sql = QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
            return Db.ExecuteReaderAsync(sql, parameters.Parse(), QueryBody.Uow);
        }

        #endregion

        #region ==ToSql==

        public string ToSql()
        {
            return QueryBuilder.GroupBySqlBuild(out IQueryParameters parameters);
        }

        public string ToSql(out IQueryParameters parameters)
        {
            return QueryBuilder.GroupBySqlBuild(out parameters);
        }

        #endregion
    }
}
