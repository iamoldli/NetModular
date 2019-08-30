using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Abstractions.Enums;
using Nm.Lib.Data.Abstractions.Pagination;
using Nm.Lib.Utils.Core;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Lib.Data.Core.SqlQueryable.Internal
{
    /// <summary>
    /// 查询主体信息
    /// </summary>
    internal class QueryBody
    {
        private readonly ISqlAdapter _sqlAdapter;

        public QueryBody(ISqlAdapter sqlAdapter)
        {
            _sqlAdapter = sqlAdapter;
        }

        #region ==属性==

        /// <summary>
        /// 事务
        /// </summary>
        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// 用户指定的表名称，对于多表连接的查询，该名称为第一张表的名称
        /// </summary>
        public string TableName { get; set; }

        public Type WhereDelegateType { get; set; }

        /// <summary>
        /// 表连接信息
        /// </summary>
        public List<QueryJoinDescriptor> JoinDescriptors { get; } = new List<QueryJoinDescriptor>();

        /// <summary>
        /// 排序
        /// </summary>
        public List<Sort> Sorts { get; } = new List<Sort>();

        /// <summary>
        /// 查询的列
        /// </summary>
        public LambdaExpression Select { get; set; }

        /// <summary>
        /// 过滤条件
        /// </summary>
        public List<LambdaExpression> Where { get; } = new List<LambdaExpression>();

        /// <summary>
        /// 更新表达式
        /// </summary>
        public LambdaExpression Update { get; set; }

        /// <summary>
        /// 设置修改人信息
        /// </summary>
        public bool SetModifiedBy { get; set; } = true;

        /// <summary>
        /// 求值表达式
        /// </summary>
        public LambdaExpression Function { get; set; }

        /// <summary>
        /// 分组条件
        /// </summary>
        public LambdaExpression GroupBy { get; set; }

        /// <summary>
        /// 分组属性集合
        /// </summary>
        public List<GroupByJoinDescriptor> GroupByPropertyList { get; private set; }

        /// <summary>
        /// 聚合
        /// </summary>
        public List<LambdaExpression> Having { get; } = new List<LambdaExpression>();

        /// <summary>
        /// 是否分组查询
        /// </summary>
        public bool IsGroupBy { get; private set; }

        /// <summary>
        /// 跳过行数
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// 取行数
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// 过滤已删除的
        /// </summary>
        public bool FilterDeleted { get; set; } = true;

        #endregion

        #region ==方法==

        public void UseTran(IDbTransaction transaction)
        {
            Transaction = transaction;
        }

        public void SetWhere(LambdaExpression whereExpression)
        {
            if (whereExpression != null)
            {
                Where.Add(whereExpression);
            }
        }

        public void SetOrderBy(LambdaExpression expression, SortType sortType = SortType.Asc)
        {
            if (expression == null)
                return;

            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                SetOrderByForMember(expression.Body as MemberExpression, expression, sortType);
                return;
            }

            if (expression.Body.NodeType == ExpressionType.New && expression.Body is NewExpression newExp)
            {
                foreach (var arg in newExp.Arguments)
                {
                    //成员
                    if (arg.NodeType == ExpressionType.MemberAccess)
                    {
                        SetOrderByForMember(arg as MemberExpression, expression, sortType);
                        continue;
                    }

                    //方法
                    if (arg is MethodCallExpression methodCallExp)
                    {
                        SetOrderByMethod(expression, methodCallExp, sortType);
                    }
                }
            }
            else if (IsGroupBy && expression.Body.NodeType == ExpressionType.Call && expression.Body is MethodCallExpression callExpression)
            {
                SetOrderByMethod(expression, callExpression, sortType);
            }
        }

        private void SetOrderByForMember(MemberExpression memberExp, LambdaExpression fullExpression, SortType sortType)
        {
            if (memberExp.Expression.NodeType == ExpressionType.MemberAccess)
            {
                //分组查询
                if (IsGroupBy)
                {
                    var descriptor = GroupByPropertyList.FirstOrDefault(m => m.Alias == memberExp.Member.Name);
                    if (descriptor != null)
                    {
                        var colName = GetColumnName(descriptor.Name, descriptor.JoinDescriptor);
                        Sorts.Add(new Sort(colName, sortType));
                    }
                }
                else
                {
                    if (memberExp.Expression.Type == typeof(string))
                    {
                        var memberName = memberExp.Member.Name.Equals("Length");
                        //解析Length函数
                        if (memberName)
                        {
                            var funcName = _sqlAdapter.FuncLength;
                            var colName = GetColumnName(memberExp.Expression as MemberExpression, fullExpression);
                            Sorts.Add(new Sort($"{funcName}({colName})", sortType));
                        }
                    }
                }
            }
            else
            {
                var colName = GetColumnName(memberExp, fullExpression);
                Sorts.Add(new Sort(colName, sortType));
            }
        }

        private void SetOrderByMethod(LambdaExpression expression, MethodCallExpression methodCallExp, SortType sortType = SortType.Asc)
        {
            var methodName = methodCallExp.Method.Name;
            if (methodName.Equals("Substring"))
            {
                SetOrderByForSubstring(methodCallExp, expression, sortType);
                return;
            }

            if (methodName.Equals("Count"))
            {
                Sorts.Add(new Sort("COUNT(0)", sortType));
                return;
            }

            if (methodName.Equals("Sum"))
            {
                ResolveSelectForFunc(methodCallExp, "SUM");
                return;
            }

            if (methodName.Equals("Avg"))
            {
                ResolveSelectForFunc(methodCallExp, "AVG");
                return;
            }

            if (methodName.Equals("Max"))
            {
                ResolveSelectForFunc(methodCallExp, "MAX");
                return;
            }

            if (methodName.Equals("Min"))
            {
                ResolveSelectForFunc(methodCallExp, "MIN");
            }
        }

        private void SetOrderByForSubstring(MethodCallExpression callExp, LambdaExpression fullExpression, SortType sortType)
        {
            if (callExp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                var funcName = _sqlAdapter.FuncSubstring;
                var colName = GetColumnName(objExp, fullExpression);
                var start = ((ConstantExpression)callExp.Arguments[0]).Value.ToInt() + 1;
                if (callExp.Arguments.Count > 1 || _sqlAdapter.SqlDialect != SqlDialect.SqlServer)
                {
                    var length = ((ConstantExpression)callExp.Arguments[1]).Value.ToInt();
                    var name = $"{funcName}({colName},{start},{length})";
                    Sorts.Add(new Sort(name, sortType));
                }
                else
                {
                    var name = $"{funcName}({colName},{start})";
                    Sorts.Add(new Sort(name, sortType));
                }
            }
        }

        /// <summary>
        /// 解析函数
        /// </summary>
        /// <param name="callExp"></param>
        /// <param name="funcName"></param>
        private void ResolveSelectForFunc(MethodCallExpression callExp, string funcName)
        {
            if (callExp.Arguments[0] is UnaryExpression unary && unary.Operand is LambdaExpression lambda)
            {
                var colName = GetColumnName(lambda.Body as MemberExpression, lambda);
                Sorts.Add(new Sort($"{funcName}({colName})"));
            }
        }

        public void SetOrderBy(Sort sort)
        {
            if (sort == null)
                return;

            Sorts.Add(sort);
        }

        public void SetOrderBy(List<Sort> sorts)
        {
            if (sorts == null)
                return;

            foreach (var sort in sorts)
            {
                SetOrderBy(sort);
            }
        }

        public void SetSelect(LambdaExpression selectExpression)
        {
            Select = selectExpression;
        }

        public void SetLimit(int skip, int take)
        {
            Skip = skip < 0 ? 0 : skip;
            Take = take < 0 ? 0 : take;
        }

        public void SetGroupBy(Expression expression)
        {
            GroupBy = expression as LambdaExpression;
            IsGroupBy = true;

            GroupByPropertyList = new List<GroupByJoinDescriptor>();
            var lambda = expression as LambdaExpression;
            if (lambda.Body.NodeType != ExpressionType.New)
                throw new ArgumentException("分组条件必须使用匿名类new{}");

            var newExp = lambda.Body as NewExpression;
            for (var i = 0; i < newExp.Members.Count; i++)
            {
                var alias = newExp.Members[i].Name;
                var member = newExp.Arguments[0] as MemberExpression;
                var parameter = member.Expression as ParameterExpression;
                var joinDescriptor = JoinDescriptors.FirstOrDefault(m => m.EntityDescriptor.EntityType == parameter.Type);
                GroupByPropertyList.Add(new GroupByJoinDescriptor
                {
                    Name = member.Member.Name,
                    Alias = alias,
                    JoinDescriptor = joinDescriptor
                });
            }
        }

        public string GetColumnName(MemberExpression exp, LambdaExpression lambda)
        {
            var index = 0;
            var memberParameter = exp.Expression as ParameterExpression;
            if (memberParameter == null)
                return string.Empty;

            foreach (var parameter in lambda.Parameters)
            {
                if (parameter.Name.Equals(memberParameter.Name))
                    break;
                index++;
            }
            var descriptor = JoinDescriptors[index];
            var name = exp.Member.Name;
            return GetColumnName(name, descriptor);
        }

        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="name"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public string GetColumnName(string name, QueryJoinDescriptor descriptor)
        {
            var col = descriptor.EntityDescriptor.Columns.FirstOrDefault(m => m.PropertyInfo.Name.Equals(name));

            Check.NotNull(col, nameof(col), $"({name})列不存在");

            //只有一个实体的时候，不需要别名
            if (JoinDescriptors.Count == 1)
            {
                // ReSharper disable once PossibleNullReferenceException
                return _sqlAdapter.AppendQuote(col.Name);
            }

            // ReSharper disable once PossibleNullReferenceException
            return $"{_sqlAdapter.AppendQuote(descriptor.Alias)}.{_sqlAdapter.AppendQuote(col.Name)}";
        }

        #endregion
    }
}
