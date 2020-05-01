using System.Linq.Expressions;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.SqlQueryable;

namespace NetModular.Lib.Data.Core.SqlQueryable.Internal
{
    /// <summary>
    /// 查询条件
    /// </summary>
    internal class QueryWhere
    {
        /// <summary>
        /// 类型
        /// </summary>
        public QueryWhereType Type { get; set; }

        /// <summary>
        /// 表达式
        /// </summary>
        public LambdaExpression Expression { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql { get; set; }

        /// <summary>
        /// 子查询的列
        /// </summary>
        public LambdaExpression SubQueryColumn { get; set; }

        /// <summary>
        /// 子查询运算符
        /// </summary>
        public QueryOperator SubQueryOperator { get; set; }

        /// <summary>
        /// 子查询
        /// </summary>
        public INetSqlQueryable SubQueryable { get; set; }

        public QueryWhere(LambdaExpression expression)
        {
            Type = QueryWhereType.LambdaExpression;
            Expression = expression;
        }

        public QueryWhere(string sql)
        {
            Type = QueryWhereType.Sql;
            Sql = sql;
        }

        public QueryWhere(LambdaExpression expression, QueryOperator queryOperator, INetSqlQueryable subQueryable)
        {
            Type = QueryWhereType.SubQuery;
            SubQueryColumn = expression;
            SubQueryOperator = queryOperator;
            SubQueryable = subQueryable;
        }
    }

    /// <summary>
    /// 查询条件类型
    /// </summary>
    public enum QueryWhereType
    {
        /// <summary>
        /// Lambda表达式
        /// </summary>
        LambdaExpression,
        /// <summary>
        /// SQL语句
        /// </summary>
        Sql,
        /// <summary>
        /// 子查询
        /// </summary>
        SubQuery
    }
}
