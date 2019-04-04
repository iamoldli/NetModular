using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable
{
    internal abstract class NetSqlQueryableAbstract : INetSqlQueryable
    {
        protected readonly ILogger Logger;
        protected readonly IDbSet Db;
        protected readonly ISqlAdapter SqlAdapter;
        protected QueryBody QueryBody;
        protected readonly QueryBuilder QueryBuilder;

        protected NetSqlQueryableAbstract(IDbSet dbSet, QueryBody queryBody)
        {
            Db = dbSet;
            SqlAdapter = dbSet.DbContext.Options.SqlAdapter;
            Logger = Db.DbContext.Options.LoggerFactory?.CreateLogger("NetSqlQueryable");

            QueryBody = queryBody;
            QueryBuilder = new QueryBuilder(QueryBody, SqlAdapter, Logger, Db.DbContext);
        }

        #region ==ToList==

        public IList<TResult> ToList<TResult>()
        {
            var sql = QueryBuilder.QuerySqlBuild(out QueryParameters parameters);
            return Db.Query<TResult>(sql, parameters.Parse()).ToList();
        }

        public async Task<IList<TResult>> ToListAsync<TResult>()
        {
            var sql = QueryBuilder.QuerySqlBuild(out QueryParameters parameters);
            return (await Db.QueryAsync<TResult>(sql, parameters.Parse())).ToList();
        }

        #endregion

        #region ==Count==

        public long Count()
        {
            var sql = QueryBuilder.CountSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<long>(sql, parameters.Parse());
        }

        public Task<long> CountAsync()
        {
            var sql = QueryBuilder.CountSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalarAsync<long>(sql, parameters.Parse());
        }

        #endregion

        #region ==First==

        public TResult First<TResult>()
        {
            var sql = QueryBuilder.FirstSqlBuild(out QueryParameters parameters);
            return Db.QueryFirstOrDefault<TResult>(sql, parameters.Parse());
        }

        public Task<TResult> FirstAsync<TResult>()
        {
            var sql = QueryBuilder.FirstSqlBuild(out QueryParameters parameters);
            return Db.QueryFirstOrDefaultAsync<TResult>(sql, parameters.Parse());
        }

        #endregion

        #region ==Pagination==

        public IList<TResult> Pagination<TResult>(Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = Count();
            return ToList<TResult>();
        }

        public async Task<IList<TResult>> PaginationAsync<TResult>(Paging paging = null)
        {
            if (paging == null)
                paging = new Paging();

            QueryBody.SetOrderBy(paging.OrderBy);
            QueryBody.SetLimit(paging.Skip, paging.Size);

            paging.TotalCount = await CountAsync();
            return await ToListAsync<TResult>();
        }

        #endregion

        #region ==Exists==

        public bool Exists()
        {
            var sql = QueryBuilder.ExistsSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<int>(sql, parameters.Parse()) > 0;
        }

        public async Task<bool> ExistsAsync()
        {
            var sql = QueryBuilder.ExistsSqlBuild(out QueryParameters parameters);
            return await Db.ExecuteScalarAsync<int>(sql, parameters.Parse()) > 0;
        }

        #endregion

        #region ==Max==

        protected TResult Max<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MaxSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse());
        }

        protected Task<TResult> MaxAsync<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MaxSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse());
        }

        #endregion

        #region ==Min==

        protected TResult Min<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MinSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse());
        }

        protected Task<TResult> MinAsync<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.MinSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse());
        }

        #endregion

        #region ==Sum==

        protected TResult Sum<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.SumSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse());
        }

        protected Task<TResult> SumAsync<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.SumSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse());
        }

        #endregion

        #region ==Avg==

        protected TResult Avg<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.AvgSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalar<TResult>(sql, parameters.Parse());
        }

        protected Task<TResult> AvgAsync<TResult>(LambdaExpression expression)
        {
            QueryBody.Function = expression;
            var sql = QueryBuilder.AvgSqlBuild(out QueryParameters parameters);
            return Db.ExecuteScalarAsync<TResult>(sql, parameters.Parse());
        }

        #endregion

        public string ToSql()
        {
            return QueryBuilder.QuerySqlBuild(out _);
        }
    }
}
