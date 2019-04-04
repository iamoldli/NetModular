using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Core.ExpressionResolve;
using NetModular.Lib.Data.Core.Internal;

namespace NetModular.Lib.Data.Core.SqlQueryable.Internal
{
    internal class QueryBuilder
    {
        #region ==字段==

        private readonly QueryBody _queryBody;
        private readonly ISqlAdapter _sqlAdapter;
        private readonly ILogger _logger;
        private readonly ExpressionResolver _resolver;
        private readonly IDbContext _dbContext;
        #endregion

        #region ==构造函数==

        public QueryBuilder(QueryBody queryBody, ISqlAdapter sqlAdapter, ILogger logger, IDbContext dbContext)
        {
            _queryBody = queryBody;
            _sqlAdapter = sqlAdapter;
            _logger = logger;
            _dbContext = dbContext;
            _resolver = new ExpressionResolver(sqlAdapter, queryBody);
        }

        #endregion

        #region ==方法==

        public string CountSqlBuild(out QueryParameters parameters)
        {
            parameters = new QueryParameters();

            var sqlBuilder = new StringBuilder("SELECT COUNT(*) FROM ");

            ResolveFrom(sqlBuilder, parameters);

            ResolveWhere(sqlBuilder, parameters);

            var sql = sqlBuilder.ToString();

            _logger?.LogDebug("Count:" + sql);

            return sql;
        }

        public string UpdateSqlBuild(string tableName, out QueryParameters parameters)
        {
            Check.NotNull(tableName, nameof(tableName), "未指定更新表");

            var sqlBuilder = new StringBuilder();
            parameters = new QueryParameters();

            var updateSql = ResolveUpdate(parameters);
            Check.NotNull(updateSql, nameof(updateSql), "生成更新sql异常");


            sqlBuilder.AppendFormat("UPDATE {0} SET ", _sqlAdapter.AppendQuote(tableName));
            sqlBuilder.Append(updateSql);

            SetModifiedBy(sqlBuilder, parameters);

            var whereSql = ResolveWhere(parameters);
            Check.NotNull(whereSql, nameof(whereSql), "生成条件sql异常");
            sqlBuilder.AppendFormat(" WHERE {0}", whereSql);

            var sql = sqlBuilder.ToString();

            _logger?.LogDebug("Update:" + sql);

            return sql;
        }

        public string DeleteSqlBuild(string tableName, out QueryParameters parameters)
        {
            Check.NotNull(tableName, nameof(tableName), "未指定更新表");

            var sqlBuilder = new StringBuilder();
            parameters = new QueryParameters();

            sqlBuilder.AppendFormat("DELETE FROM {0} ", _sqlAdapter.AppendQuote(tableName));

            var whereSql = ResolveWhere(parameters);
            Check.NotNull(whereSql, nameof(whereSql), "生成条件sql异常");
            sqlBuilder.AppendFormat(" WHERE {0}", whereSql);

            var sql = sqlBuilder.ToString();

            _logger?.LogDebug("Delete:" + sql);

            return sql;
        }

        public string SoftDeleteSqlBuild(string tableName, out QueryParameters parameters)
        {
            Check.NotNull(tableName, nameof(tableName), "未指定删除表");

            parameters = new QueryParameters();

            var sqlBuilder = new StringBuilder($"UPDATE {_sqlAdapter.AppendQuote(tableName)} SET ");
            sqlBuilder.AppendFormat("{0}=1,", _sqlAdapter.AppendQuote("Deleted"));
            sqlBuilder.AppendFormat("{0}={1},", _sqlAdapter.AppendQuote("DeletedTime"), _sqlAdapter.AppendParameter("P1"));
            parameters.Add(DateTime.Now);
            sqlBuilder.AppendFormat("{0}={1} ", _sqlAdapter.AppendQuote("DeletedBy"), _sqlAdapter.AppendParameter("P2"));
            parameters.Add(_dbContext.AccountId);

            var whereSql = ResolveWhere(parameters);
            Check.NotNull(whereSql, nameof(whereSql), "生成条件sql异常");
            sqlBuilder.AppendFormat(" WHERE {0}", whereSql);

            var sql = sqlBuilder.ToString();

            _logger?.LogDebug("SoftDelete:" + sql);

            return sql;
        }

        public string MaxSqlBuild(out QueryParameters parameters)
        {
            return FuncSqlBuild("MAX", out parameters);
        }

        public string MinSqlBuild(out QueryParameters parameters)
        {
            return FuncSqlBuild("MIN", out parameters);
        }

        public string SumSqlBuild(out QueryParameters parameters)
        {
            return FuncSqlBuild("SUM", out parameters);
        }

        public string AvgSqlBuild(out QueryParameters parameters)
        {
            return FuncSqlBuild("Avg", out parameters);
        }

        /// <summary>
        /// 通用函数解析方法
        /// </summary>
        /// <param name="funcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string FuncSqlBuild(string funcName, out QueryParameters parameters)
        {
            var func = _queryBody.Function;
            Check.NotNull(func, nameof(func), "函数解析失败");

            var memberExpression = func.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("无法解析表达式", nameof(func));

            var sqlBuilder = new StringBuilder("SELECT ");
            parameters = new QueryParameters();
            sqlBuilder.AppendFormat("{0}({1}) FROM ", funcName, _queryBody.GetColumnName(memberExpression, func));

            ResolveFrom(sqlBuilder, parameters);

            ResolveWhere(sqlBuilder, parameters);

            var sql = sqlBuilder.ToString();

            _logger?.LogDebug($"{funcName}:{sql}");

            return sql;
        }

        public string FirstSqlBuild(out QueryParameters parameters)
        {
            parameters = new QueryParameters();
            var select = ResolveSelect();
            var from = ResolveFrom(parameters);
            var where = ResolveWhere(parameters);
            var sort = ResolveOrder();

            var sql = _sqlAdapter.GenerateFirstSql(select, from, where, sort);

            _logger?.LogDebug("First:" + sql);

            return sql;
        }

        public string ExistsSqlBuild(out QueryParameters parameters)
        {
            parameters = new QueryParameters();

            var select = "1";
            var from = ResolveFrom(parameters);
            var where = ResolveWhere(parameters);
            var sort = ResolveOrder();

            var sql = _sqlAdapter.GenerateFirstSql(select, from, where, sort);

            _logger?.LogDebug("Exists:{0}", sql);

            return sql;
        }

        public string QuerySqlBuild(out QueryParameters parameters)
        {
            string sql;
            parameters = new QueryParameters();

            //分页查询
            if (_queryBody.Take > 0)
            {
                var select = ResolveSelect();
                var from = ResolveFrom(parameters);
                var where = ResolveWhere(parameters);
                var sort = ResolveOrder();

                #region ==SqlServer分页需要指定排序==

                //SqlServer分页需要指定排序，此处判断是否有主键，有主键默认按照主键排序
                if (_sqlAdapter.SqlDialect == SqlDialect.SqlServer && sort.IsNull())
                {
                    var first = _queryBody.JoinDescriptors.First();
                    if (first.EntityDescriptor.PrimaryKey.IsNo())
                    {
                        throw new Exception("SqlServer数据库没有主键的表需要指定排序字段才可以分页查询");
                    }

                    if (_queryBody.JoinDescriptors.Count > 1)
                    {
                        sort = $"{_sqlAdapter.AppendQuote(first.Alias)}.{_sqlAdapter.AppendQuote(first.EntityDescriptor.PrimaryKey.Name)}";
                    }
                    else
                    {
                        sort = first.EntityDescriptor.PrimaryKey.Name;
                    }
                }

                #endregion

                sql = _sqlAdapter.GeneratePagingSql(select, from, where, sort, _queryBody.Skip, _queryBody.Take);
            }
            else
            {
                var sqlBuilder = new StringBuilder("SELECT ");

                ResolveSelect(sqlBuilder);

                sqlBuilder.Append(" FROM ");

                ResolveFrom(sqlBuilder, parameters);

                ResolveWhere(sqlBuilder, parameters);

                ResolveOrder(sqlBuilder);

                sql = sqlBuilder.ToString();
            }

            _logger?.LogDebug("Query:{0}", sql);

            return sql;
        }

        public string GroupBySqlBuild(out QueryParameters parameters)
        {
            parameters = new QueryParameters();
            var sqlBuilder = new StringBuilder("SELECT ");

            Check.NotNull(_queryBody.Select, nameof(_queryBody.Select), "未指定Select");
            ResolveSelect(sqlBuilder);

            sqlBuilder.Append(" FROM ");

            ResolveFrom(sqlBuilder, parameters);

            ResolveWhere(sqlBuilder, parameters);

            ResolveGroupBy(sqlBuilder);

            ResolveHaving(sqlBuilder, parameters);

            ResolveOrder(sqlBuilder);

            string sql = sqlBuilder.ToString();
            _logger?.LogDebug("GroupBy:{0}", sql);

            return sql;
        }

        #endregion

        #region ==解析Body==

        private string ResolveFrom(QueryParameters parameters)
        {
            var sqlBuilder = new StringBuilder();
            ResolveFrom(sqlBuilder, parameters);
            return sqlBuilder.ToString();
        }

        private void ResolveFrom(StringBuilder sqlBuilder, QueryParameters parameters)
        {
            var first = _queryBody.JoinDescriptors.First();

            if (_queryBody.JoinDescriptors.Count == 1)
            {
                sqlBuilder.AppendFormat(" {0}{1} ", first.EntityDescriptor.SqlAdapter.Database, _sqlAdapter.AppendQuote(first.EntityDescriptor.TableName));
                return;
            }

            sqlBuilder.AppendFormat(" {0}{1} AS {2} ", first.EntityDescriptor.SqlAdapter.Database, _sqlAdapter.AppendQuote(first.EntityDescriptor.TableName), _sqlAdapter.AppendQuote(first.Alias));

            for (var i = 1; i < _queryBody.JoinDescriptors.Count; i++)
            {
                var descriptor = _queryBody.JoinDescriptors[i];
                switch (descriptor.Type)
                {
                    case JoinType.Inner:
                        sqlBuilder.Append(" INNER ");
                        break;
                    case JoinType.Right:
                        sqlBuilder.Append(" RIGHT ");
                        break;
                    default:
                        sqlBuilder.Append(" LEFT ");
                        break;
                }
                sqlBuilder.AppendFormat("JOIN {0}{1} AS {2} ON ", descriptor.EntityDescriptor.SqlAdapter.Database, _sqlAdapter.AppendQuote(descriptor.EntityDescriptor.TableName), _sqlAdapter.AppendQuote(descriptor.Alias));

                sqlBuilder.Append(_resolver.Resolve(descriptor.On, parameters));
            }
        }

        private string ResolveWhere(QueryParameters parameters)
        {
            var whereSql = new StringBuilder();
            for (var i = 0; i < _queryBody.Where.Count; i++)
            {
                var w = _queryBody.Where[i];
                whereSql.Append(_resolver.Resolve(w, parameters));
                if (i < _queryBody.Where.Count - 1)
                {
                    whereSql.Append(" AND ");
                }
            }

            return whereSql.ToString();
        }

        private void ResolveWhere(StringBuilder sqlBuilder, QueryParameters parameters)
        {
            if (_queryBody.Where == null)
                return;

            var whereSql = ResolveWhere(parameters);
            if (whereSql.Length > 0)
            {
                sqlBuilder.AppendFormat(" WHERE {0}", whereSql);
            }
        }

        private string ResolveUpdate(QueryParameters parameters)
        {
            Check.NotNull(_queryBody.Update, nameof(_queryBody.Update), "未指定更新字段");

            var sql = _resolver.Resolve(_queryBody.Update, parameters);

            Check.NotNull(sql, nameof(_queryBody.Update), "更新表达式解析失败");

            return sql;
        }

        private string ResolveOrder()
        {
            var sqlBuilder = new StringBuilder();
            if (_queryBody.Sorts.Any())
            {
                _queryBody.Sorts.ForEach(sort =>
                {
                    sqlBuilder.AppendFormat(" {0} {1},", sort.OrderBy, sort.Type == SortType.Asc ? "ASC" : "DESC");
                });

                sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
            }

            return sqlBuilder.ToString();
        }

        private void ResolveOrder(StringBuilder sqlBuilder)
        {
            var sql = ResolveOrder();
            if (sql.NotNull())
                sqlBuilder.AppendFormat(" ORDER BY {0}", sql);
        }

        private string ResolveSelect()
        {
            var sqlBuilder = new StringBuilder();

            ResolveSelect(sqlBuilder, _queryBody.Select);

            return sqlBuilder.ToString();
        }

        private void ResolveSelect(StringBuilder sqlBuilder)
        {
            ResolveSelect(sqlBuilder, _queryBody.Select);
        }

        private void ResolveSelect(StringBuilder sqlBuilder, Expression selectExpression)
        {
            if (selectExpression is LambdaExpression lambda)
            {
                //返回的整个实体
                if (lambda.Body.NodeType == ExpressionType.Parameter)
                {
                    ResolveSelectForEntity(sqlBuilder);
                    return;
                }
                //返回的某个列
                if (lambda.Body.NodeType == ExpressionType.MemberAccess)
                {
                    ResolveSelectForMember(sqlBuilder, lambda.Body, lambda);
                    if (sqlBuilder.Length > 0 && sqlBuilder[sqlBuilder.Length - 1] == ',')
                    {
                        sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
                    }
                    return;
                }
                //自定义的返回对象
                if (lambda.Body.NodeType == ExpressionType.New)
                {
                    ResolveSelectForNew(sqlBuilder, lambda);
                }
            }
            else
            {
                ResolveSelectForEntity(sqlBuilder);
            }

            if (sqlBuilder[sqlBuilder.Length - 1] == ',')
            {
                sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
            }
        }

        private void ResolveSelectForNew(StringBuilder sqlBuilder, LambdaExpression fullExpression)
        {
            if (!(fullExpression.Body is NewExpression newExp))
                return;

            for (var i = 0; i < newExp.Arguments.Count; i++)
            {
                var arg = newExp.Arguments[i];
                var alias = _sqlAdapter.AppendQuote(newExp.Members[i].Name);
                //成员
                if (arg.NodeType == ExpressionType.MemberAccess)
                {
                    ResolveSelectForMember(sqlBuilder, arg, fullExpression, alias);
                    continue;
                }
                //实体
                if (arg is ParameterExpression parameterExp)
                {
                    ResolveSelectForEntity(sqlBuilder, fullExpression.Parameters.IndexOf(parameterExp));
                    continue;
                }
                //方法
                if (arg is MethodCallExpression methodCallExp)
                {
                    var methodName = methodCallExp.Method.Name;
                    if (methodName.Equals("Substring"))
                    {
                        ResolveSelectForSubstring(methodCallExp, sqlBuilder, fullExpression, alias);
                        continue;
                    }
                    if (methodName.Equals("ToLower"))
                    {
                        ResolveSelectForToLower(methodCallExp, sqlBuilder, fullExpression, alias);
                        continue;
                    }
                    if (methodName.Equals("ToUpper"))
                    {
                        ResolveSelectForToUpper(methodCallExp, sqlBuilder, fullExpression, alias);
                        continue;
                    }
                    if (methodName.Equals("Count"))
                    {
                        sqlBuilder.AppendFormat("COUNT(0) AS {0},", alias);
                        continue;
                    }

                    if (methodName.Equals("Sum"))
                    {
                        ResolveSelectForFunc(methodCallExp, sqlBuilder, "SUM", alias);
                        continue;
                    }
                    if (methodName.Equals("Avg"))
                    {
                        ResolveSelectForFunc(methodCallExp, sqlBuilder, "AVG", alias);
                        continue;
                    }
                    if (methodName.Equals("Max"))
                    {
                        ResolveSelectForFunc(methodCallExp, sqlBuilder, "MAX", alias);
                        continue;
                    }
                    if (methodName.Equals("Min"))
                    {
                        ResolveSelectForFunc(methodCallExp, sqlBuilder, "MIN", alias);
                    }
                }
            }
        }

        /// <summary>
        /// 解析查询列中的成员表达式
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="exp"></param>
        /// <param name="fullExpression"></param>
        /// <param name="alias"></param>
        private void ResolveSelectForMember(StringBuilder sqlBuilder, Expression exp, LambdaExpression fullExpression, string alias = null)
        {
            if (!(exp is MemberExpression memberExp))
                return;

            alias = alias ?? memberExp.Member.Name;
            if (memberExp.Expression.NodeType == ExpressionType.MemberAccess)
            {
                //分组查询
                if (_queryBody.IsGroupBy)
                {
                    var descriptor = _queryBody.GroupByPropertyList.FirstOrDefault(m => _sqlAdapter.AppendQuote(m.Alias) == alias);
                    if (descriptor != null)
                    {
                        var colName = _queryBody.GetColumnName(descriptor.Name, descriptor.JoinDescriptor);
                        sqlBuilder.AppendFormat("{0} AS {1},", colName, alias);
                    }
                }
                else
                {
                    if (memberExp.Expression.Type == typeof(string))
                    {
                        var memberName = memberExp.Member.Name;
                        //解析Length函数
                        if (memberName.Equals("Length"))
                        {
                            var funcName = _sqlAdapter.FuncLength;
                            var colName = _queryBody.GetColumnName(memberExp.Expression as MemberExpression, fullExpression);
                            sqlBuilder.AppendFormat("{0}({1}) AS {2},", funcName, colName, alias);
                        }
                    }
                }
            }
            else
            {
                var colName = _queryBody.GetColumnName(memberExp, fullExpression);
                sqlBuilder.AppendFormat("{0} AS {1},", colName, alias);
            }
        }

        /// <summary>
        /// 解析查询列中的整个实体
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="descriptorIndex">实体的下标</param>
        private void ResolveSelectForEntity(StringBuilder sqlBuilder, int descriptorIndex = 0)
        {
            var descriptor = _queryBody.JoinDescriptors[descriptorIndex];

            for (var i = 0; i < descriptor.EntityDescriptor.Columns.Count; i++)
            {
                var col = descriptor.EntityDescriptor.Columns[i];
                //单表时不需要别名
                var isSingleTable = _queryBody.JoinDescriptors.Count <= 1;
                sqlBuilder.Append(isSingleTable
                    ? $"{_sqlAdapter.AppendQuote(col.Name)}"
                    : $"{_sqlAdapter.AppendQuote(descriptor.Alias)}.{_sqlAdapter.AppendQuote(col.Name)}");

                sqlBuilder.AppendFormat(" AS {0},", _sqlAdapter.AppendQuote(col.PropertyInfo.Name));
            }
        }

        /// <summary>
        /// 解析查询列中的字符串截取函数
        /// </summary>
        /// <param name="callExp"></param>
        /// <param name="sqlBuilder"></param>
        /// <param name="fullExpression"></param>
        /// <param name="alias"></param>
        private void ResolveSelectForSubstring(MethodCallExpression callExp, StringBuilder sqlBuilder, LambdaExpression fullExpression, string alias)
        {
            if (callExp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                var funcName = _sqlAdapter.FuncSubstring;
                var colName = _queryBody.GetColumnName(objExp, fullExpression);
                var start = ((ConstantExpression)callExp.Arguments[0]).Value.ToInt() + 1;
                if (callExp.Arguments.Count > 1)
                {
                    var length = ((ConstantExpression)callExp.Arguments[1]).Value.ToInt();
                    sqlBuilder.AppendFormat("{0}({1},{2},{3}) AS {4},", funcName, colName, start, length, alias);
                }
                else
                {
                    if (_sqlAdapter.SqlDialect == SqlDialect.SqlServer)
                    {
                        sqlBuilder.AppendFormat("{0}({1},{2},{3}) AS {4},", funcName, colName, start, $"LEN({colName})-{start - 1}", alias);
                    }
                    else
                    {
                        sqlBuilder.AppendFormat("{0}({1},{2}) AS {3},", funcName, colName, start, alias);
                    }
                }
            }
        }

        /// <summary>
        /// 解析转小写
        /// </summary>
        private void ResolveSelectForToLower(MethodCallExpression callExp, StringBuilder sqlBuilder, LambdaExpression fullExpression, string alias)
        {
            if (callExp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                var funcName = _sqlAdapter.FuncLower;
                var colName = _queryBody.GetColumnName(objExp, fullExpression);
                sqlBuilder.AppendFormat("{0}({1}) AS {2},", funcName, colName, alias);
            }
        }

        /// <summary>
        /// 解析转大写
        /// </summary>
        private void ResolveSelectForToUpper(MethodCallExpression callExp, StringBuilder sqlBuilder, LambdaExpression fullExpression, string alias)
        {
            if (callExp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                var funcName = _sqlAdapter.FuncUpper;
                var colName = _queryBody.GetColumnName(objExp, fullExpression);
                sqlBuilder.AppendFormat("{0}({1}) AS {2},", funcName, colName, alias);
            }
        }

        /// <summary>
        /// 解析函数
        /// </summary>
        /// <param name="callExp"></param>
        /// <param name="sqlBuilder"></param>
        /// <param name="funcName"></param>
        /// <param name="alias"></param>
        private void ResolveSelectForFunc(MethodCallExpression callExp, StringBuilder sqlBuilder, string funcName, string alias)
        {
            if (callExp.Arguments[0] is UnaryExpression unary && unary.Operand is LambdaExpression lambda)
            {
                var colName = _queryBody.GetColumnName(lambda.Body as MemberExpression, lambda);
                sqlBuilder.AppendFormat("{0}({1}) AS {2},", funcName, colName, alias);
            }
        }

        /// <summary>
        /// 解析分组条件
        /// </summary>
        /// <param name="sqlBuilder"></param>
        private void ResolveGroupBy(StringBuilder sqlBuilder)
        {
            sqlBuilder.Append(" GROUP BY ");

            var list = _queryBody.GroupByPropertyList;
            var i = 0;
            foreach (var p in list)
            {
                var colName = _queryBody.GetColumnName(p.Name, p.JoinDescriptor);
                sqlBuilder.AppendFormat("{0}", colName);

                if (i < list.Count - 1)
                {
                    sqlBuilder.Append(",");
                }

                i++;
            }
        }

        /// <summary>
        /// 解析聚合过滤条件
        /// </summary>
        private void ResolveHaving(StringBuilder sqlBuilder, QueryParameters parameters)
        {
            var havingSql = new StringBuilder();
            for (var i = 0; i < _queryBody.Having.Count; i++)
            {
                var w = _queryBody.Having[i];
                havingSql.Append(_resolver.Resolve(w, parameters));
                if (i < _queryBody.Where.Count - 1)
                {
                    havingSql.Append(" AND ");
                }
            }

            sqlBuilder.AppendFormat(" HAVING {0} ", havingSql);
        }
        #endregion

        /// <summary>
        /// 设置修改人和修改时间
        /// </summary>
        private void SetModifiedBy(StringBuilder sqlBuilder, QueryParameters parameters)
        {
            if (!_queryBody.SetModifiedBy || _dbContext.AccountId.IsNull())
                return;

            var descriptor = _queryBody.JoinDescriptors.FirstOrDefault()?.EntityDescriptor;
            if (descriptor != null && descriptor.IsEntityBase)
            {
                var p1 = parameters.Add(new Guid(_dbContext.AccountId));
                sqlBuilder.AppendFormat(",{0}=@{1}", _sqlAdapter.AppendQuote("ModifiedBy"), p1);
                var p2 = parameters.Add(DateTime.Now);
                sqlBuilder.AppendFormat(",{0}=@{1}", _sqlAdapter.AppendQuote("ModifiedTime"), p2);
            }
        }
    }
}
