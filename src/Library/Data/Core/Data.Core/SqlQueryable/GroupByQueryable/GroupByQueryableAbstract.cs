using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Pagination;
using NetModular.Lib.Data.Abstractions.SqlQueryable.GroupByQueryable;
using NetModular.Lib.Data.Core.Internal;
using NetModular.Lib.Data.Core.SqlQueryable.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.GroupByQueryable
{
    internal abstract class GroupByQueryableAbstract : IGroupByQueryable
    {
        protected readonly IDbSet _db;
        protected readonly QueryBody _queryBody;
        protected readonly QueryBuilder _queryBuilder;

        protected GroupByQueryableAbstract(IDbSet db, QueryBody queryBody, QueryBuilder queryBuilder, Expression expression)
        {
            _db = db;
            _queryBody = queryBody;
            _queryBuilder = queryBuilder;
            _queryBody.SetGroupBy(expression);
        }

        protected void SetHaving(LambdaExpression expression)
        {
            if (expression != null)
                _queryBody.Having.Add(expression);
        }

        public void SetOrderBy(string customOrderBy)
        {
            if (customOrderBy.NotNull())
                _queryBody.SetOrderBy(new Sort(customOrderBy));
        }

        public void SetOrderByDescending(string customOrderBy)
        {
            if (customOrderBy.NotNull())
                _queryBody.SetOrderBy(new Sort(customOrderBy, SortType.Desc));
        }

        public void SetOrderBy(LambdaExpression expression)
        {
            if (expression != null)
                _queryBody.SetOrderBy(expression);
        }

        public void SetOrderByDescending(LambdaExpression expression)
        {
            if (expression != null)
                _queryBody.SetOrderBy(expression, SortType.Desc);
        }

        public void SetSelect(LambdaExpression expression)
        {
            if (expression != null)
                _queryBody.Select = expression;
        }

        public IList<TResult> ToList<TResult>()
        {
            var sql = _queryBuilder.GroupBySqlBuild(out QueryParameters parameters);
            return _db.Query<TResult>(sql, parameters.Parse()).ToList();
        }

        public async Task<IList<TResult>> ToListAsync<TResult>()
        {
            var sql = _queryBuilder.GroupBySqlBuild(out QueryParameters parameters);
            return (await _db.QueryAsync<TResult>(sql, parameters.Parse())).ToList();
        }
    }
}
