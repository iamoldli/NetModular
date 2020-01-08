using System.Linq.Expressions;

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
        Sql
    }
}
