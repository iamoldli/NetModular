using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Abstractions.Enums;
using Tm.Lib.Data.Core.Internal;
using Tm.Lib.Data.Core.SqlQueryable.Internal;

namespace Tm.Lib.Data.Core.ExpressionResolve
{
    internal class ExpressionResolver
    {
        private readonly ISqlAdapter _sqlAdapter;
        private readonly QueryBody _queryBody;
        private LambdaExpression _fullExpression;
        private IQueryParameters _parameters;
        private StringBuilder _sqlBuilder;

        public ExpressionResolver(ISqlAdapter sqlAdapter, QueryBody queryBody)
        {
            _sqlAdapter = sqlAdapter;
            _queryBody = queryBody;
        }

        public string Resolve(LambdaExpression expression, IQueryParameters parameters)
        {
            if (expression == null)
                return string.Empty;

            _fullExpression = expression;
            _parameters = parameters;
            _sqlBuilder = new StringBuilder();

            Resolve(_fullExpression);

            return _sqlBuilder.ToString();
        }

        private void Resolve(Expression exp)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Lambda:
                    LambdaResolve(exp);
                    break;
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    BinaryResolve(exp);
                    break;
                case ExpressionType.Constant:
                    ConstantResolve(exp);
                    break;
                case ExpressionType.MemberAccess:
                    MemberAccessResolve(exp);
                    break;
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    UnaryResolve(exp);
                    break;
                case ExpressionType.Call:
                    CallResolve(exp);
                    break;
                case ExpressionType.Not:
                    NotResolve(exp);
                    break;
                case ExpressionType.MemberInit:
                    MemberInitResolve(exp);
                    break;
            }
        }

        private void LambdaResolve(Expression exp)
        {
            if (exp == null || !(exp is LambdaExpression lambdaExp))
                return;

            Resolve(lambdaExp.Body);
        }

        private void BinaryResolve(Expression exp)
        {
            if (exp == null || !(exp is BinaryExpression binaryExp))
                return;

            _sqlBuilder.Append("(");

            Resolve(binaryExp.Left);

            switch (binaryExp.NodeType)
            {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    _sqlBuilder.Append(" AND ");
                    break;
                case ExpressionType.GreaterThan:
                    _sqlBuilder.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _sqlBuilder.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    _sqlBuilder.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _sqlBuilder.Append(" <= ");
                    break;
                case ExpressionType.Equal:
                    _sqlBuilder.Append(" = ");
                    break;
                case ExpressionType.OrElse:
                case ExpressionType.Or:
                    _sqlBuilder.Append(" OR ");
                    break;
                case ExpressionType.NotEqual:
                    _sqlBuilder.Append(" <> ");
                    break;
            }

            Resolve(binaryExp.Right);

            _sqlBuilder.Append(")");
        }

        private void ConstantResolve(Expression exp)
        {
            if (exp == null || !(exp is ConstantExpression constantExp))
                return;

            AppendValue(constantExp.Value);
        }

        private void MemberAccessResolve(Expression exp)
        {
            if (exp == null || !(exp is MemberExpression memberExp))
                return;

            if (memberExp.Expression != null)
            {
                if (memberExp.Expression.NodeType == ExpressionType.Parameter)
                {
                    _sqlBuilder.Append(_queryBody.GetColumnName(memberExp, _fullExpression));
                    return;
                }

                if (memberExp.Expression.NodeType == ExpressionType.Constant)
                {
                    DynamicInvokeResolve(exp);
                    return;
                }
                if (memberExp.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    //分组查询
                    if (_queryBody.IsGroupBy)
                    {
                        var descriptor = _queryBody.GroupByPropertyList.FirstOrDefault(m => m.Alias == memberExp.Member.Name);
                        if (descriptor != null)
                        {
                            var colName = _queryBody.GetColumnName(descriptor.Name, descriptor.JoinDescriptor);
                            _sqlBuilder.AppendFormat("{0}", colName);
                            return;
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
                                var colName = _queryBody.GetColumnName(memberExp.Expression as MemberExpression, _fullExpression);
                                _sqlBuilder.AppendFormat("{0}({1})", funcName, colName);
                                return;
                            }
                        }
                    }
                }
            }

            //对于非实体属性的成员，如外部变量等
            DynamicInvokeResolve(exp);
        }

        private void UnaryResolve(Expression exp)
        {
            if (exp == null || !(exp is UnaryExpression unaryExp))
                return;

            Resolve(unaryExp.Operand);
        }

        private void DynamicInvokeResolve(Expression exp)
        {
            var value = DynamicInvoke(exp);

            AppendValue(value);
        }

        #region ==函数解析==

        private void CallResolve(Expression exp)
        {
            if (exp == null || !(exp is MethodCallExpression callExp))
                return;

            var methodName = callExp.Method.Name;
            if (methodName.Equals("StartsWith"))
            {
                StartsWithResolve(callExp);
                return;
            }
            if (methodName.Equals("EndsWith"))
            {
                EndsWithResolve(callExp);
                return;
            }
            if (methodName.Equals("Contains"))
            {
                ContainsResolve(callExp);
                return;
            }
            if (methodName.Equals("Equals"))
            {
                EqualsResolve(callExp);
                return;
            }
            if (methodName.Equals("Substring"))
            {
                SubstringResolve(callExp);
                return;
            }

            if (_queryBody.IsGroupBy)
            {
                if (methodName.Equals("Count"))
                {
                    _sqlBuilder.Append("COUNT(0)");
                    return;
                }

                if (methodName.Equals("Sum"))
                {
                    ResolveSelectForFunc(callExp, "SUM");
                    return;
                }

                if (methodName.Equals("Avg"))
                {
                    ResolveSelectForFunc(callExp, "AVG");
                    return;
                }

                if (methodName.Equals("Max"))
                {
                    ResolveSelectForFunc(callExp, "MAX");
                    return;
                }

                if (methodName.Equals("Min"))
                {
                    ResolveSelectForFunc(callExp, "MIN");
                    return;
                }
            }

            DynamicInvokeResolve(exp);
        }

        private void StartsWithResolve(MethodCallExpression exp)
        {
            if (exp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                _sqlBuilder.Append(_queryBody.GetColumnName(objExp, _fullExpression));

                string value;
                if (exp.Arguments[0] is ConstantExpression c)
                {
                    value = c.Value.ToString();
                }
                else
                {
                    value = DynamicInvoke(exp.Arguments[0]).ToString();
                }

                _sqlBuilder.Append(" LIKE ");

                AppendValue($"{value}%");
            }
        }

        private void EndsWithResolve(MethodCallExpression exp)
        {
            if (exp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                _sqlBuilder.Append(_queryBody.GetColumnName(objExp, _fullExpression));

                string value;
                if (exp.Arguments[0] is ConstantExpression c)
                {
                    value = c.Value.ToString();
                }
                else
                {
                    value = DynamicInvoke(exp.Arguments[0]).ToString();
                }

                _sqlBuilder.Append(" LIKE ");

                AppendValue($"%{value}");
            }
        }

        private void ContainsResolve(MethodCallExpression exp)
        {
            if (exp.Object is MemberExpression objExp)
            {
                if (objExp.Expression.NodeType == ExpressionType.Parameter)
                {
                    _sqlBuilder.Append(_queryBody.GetColumnName(objExp, _fullExpression));

                    string value;
                    if (exp.Arguments[0] is ConstantExpression c)
                    {
                        value = c.Value.ToString();
                    }
                    else
                    {
                        value = DynamicInvoke(exp.Arguments[0]).ToString();
                    }

                    _sqlBuilder.Append(" LIKE ");

                    AppendValue($"%{value}%");
                }
                else if (objExp.Type.IsGenericType && exp.Arguments[0] is MemberExpression argExp && argExp.Expression.NodeType == ExpressionType.Parameter)
                {
                    _sqlBuilder.Append(_queryBody.GetColumnName(argExp, _fullExpression));

                    _sqlBuilder.Append(" IN (");

                    #region ==解析集合==

                    if (objExp.Expression is ConstantExpression constant)
                    {
                        var value = ((FieldInfo)objExp.Member).GetValue(constant.Value);
                        var valueType = objExp.Type.GetGenericArguments()[0];
                        var isValueType = false;
                        var list = new List<string>();
                        if (valueType == typeof(string))
                        {
                            list = value as List<string>;
                        }
                        else if (valueType == typeof(Guid))
                        {
                            if (value is List<Guid> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString());
                                }
                            }
                        }
                        else if (valueType == typeof(char))
                        {
                            if (value is List<char> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString());
                                }
                            }
                        }
                        else if (valueType == typeof(DateTime))
                        {
                            if (value is List<DateTime> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString("yyyy-MM-dd HH:mm:ss"));
                                }
                            }
                        }
                        else if (valueType == typeof(int))
                        {
                            isValueType = true;
                            if (value is List<int> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString());
                                }
                            }
                        }
                        else if (valueType == typeof(long))
                        {
                            isValueType = true;
                            if (value is List<long> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString());
                                }
                            }
                        }
                        else if (valueType == typeof(double))
                        {
                            isValueType = true;
                            if (value is List<double> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString(CultureInfo.InvariantCulture));
                                }
                            }
                        }
                        else if (valueType == typeof(float))
                        {
                            isValueType = true;
                            if (value is List<float> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString(CultureInfo.InvariantCulture));
                                }
                            }
                        }
                        else if (valueType == typeof(decimal))
                        {
                            isValueType = true;
                            if (value is List<decimal> valueList)
                            {
                                foreach (var c in valueList)
                                {
                                    list.Add(c.ToString(CultureInfo.InvariantCulture));
                                }
                            }
                        }

                        if (list == null)
                            return;


                        //值类型不带引号
                        if (isValueType)
                        {
                            for (var i = 0; i < list.Count; i++)
                            {
                                _sqlBuilder.AppendFormat("{0}", list[i]);
                                if (i != list.Count - 1)
                                {
                                    _sqlBuilder.Append(",");
                                }
                            }
                        }
                        else
                        {
                            for (var i = 0; i < list.Count; i++)
                            {
                                _sqlBuilder.AppendFormat("'{0}'", list[i].Replace("'", "''"));
                                if (i != list.Count - 1)
                                {
                                    _sqlBuilder.Append(",");
                                }
                            }
                        }
                    }

                    #endregion

                    _sqlBuilder.Append(") ");
                }
            }
            else if (exp.Arguments[0].Type.IsArray && exp.Arguments[1] is MemberExpression argExp && argExp.Expression.NodeType == ExpressionType.Parameter)
            {
                _sqlBuilder.Append(_queryBody.GetColumnName(argExp, _fullExpression));
                _sqlBuilder.Append(" IN (");

                #region ==解析数组==

                var member = exp.Arguments[0] as MemberExpression;
                var constant = member.Expression as ConstantExpression;
                var value = ((FieldInfo)member.Member).GetValue(constant.Value);
                var valueType = member.Type.FullName;
                var isValueType = false;
                string[] list = null;
                if (valueType == "System.String[]")
                {
                    list = value as string[];
                }
                else if (valueType == "System.Guid[]")
                {
                    if (value is Guid[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString();
                        }
                    }
                }
                else if (valueType == "System.Char[]")
                {
                    if (value is char[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString();
                        }
                    }
                }
                else if (valueType == "System.Datetime[]")
                {
                    if (value is DateTime[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }
                else if (valueType == "System.Int32[]")
                {
                    isValueType = true;
                    if (value is int[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString();
                        }
                    }
                }
                else if (valueType == "System.Int64[]")
                {
                    isValueType = true;
                    if (value is long[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString();
                        }
                    }
                }
                else if (valueType == "System.Double[]")
                {
                    isValueType = true;
                    if (value is double[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString(CultureInfo.InvariantCulture);
                        }
                    }
                }
                else if (valueType == "System.Single[]")
                {
                    isValueType = true;
                    if (value is float[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString(CultureInfo.InvariantCulture);
                        }
                    }
                }
                else if (valueType == "System.Decimal[]")
                {
                    isValueType = true;
                    if (value is decimal[] valueList)
                    {
                        list = new string[valueList.Length];
                        for (var i = 0; i < valueList.Length; i++)
                        {
                            list[i] = valueList[i].ToString(CultureInfo.InvariantCulture);
                        }
                    }
                }

                if (list == null)
                    return;

                //值类型不带引号
                if (isValueType)
                {
                    for (var i = 0; i < list.Length; i++)
                    {
                        _sqlBuilder.AppendFormat("{0}", list[i]);
                        if (i != list.Length - 1)
                        {
                            _sqlBuilder.Append(",");
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < list.Length; i++)
                    {
                        _sqlBuilder.AppendFormat("'{0}'", list[i].Replace("'", "''"));
                        if (i != list.Length - 1)
                        {
                            _sqlBuilder.Append(",");
                        }
                    }
                }

                #endregion

                _sqlBuilder.Append(") ");
            }
        }

        private void EqualsResolve(MethodCallExpression exp)
        {
            if (exp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                _sqlBuilder.Append(_queryBody.GetColumnName(objExp, _fullExpression));

                _sqlBuilder.Append(" = ");

                var arg = exp.Arguments[0];
                if (arg is ConstantExpression c)
                {
                    AppendValue(c.Value.ToString());
                }
                else if (arg.NodeType == ExpressionType.MemberAccess)
                {
                    MemberAccessResolve(arg);
                }
                else if (arg.NodeType == ExpressionType.Convert)
                {
                    UnaryResolve(arg);
                }
                else
                {
                    AppendValue(DynamicInvoke(arg).ToString());
                }
            }
        }

        private void SubstringResolve(MethodCallExpression exp)
        {
            if (exp.Object is MemberExpression objExp && objExp.Expression.NodeType == ExpressionType.Parameter)
            {
                var funcName = _sqlAdapter.FuncSubstring;
                var colName = _queryBody.GetColumnName(objExp, _fullExpression);
                var start = ((ConstantExpression)exp.Arguments[0]).Value.ToInt() + 1;
                if (exp.Arguments.Count > 1)
                {
                    var length = ((ConstantExpression)exp.Arguments[1]).Value.ToInt();
                    _sqlBuilder.AppendFormat("{0}({1},{2},{3})", funcName, colName, start, length);
                }
                else
                {
                    if (_sqlAdapter.SqlDialect == SqlDialect.SqlServer)
                    {
                        _sqlBuilder.AppendFormat("{0}({1},{2},{3})", funcName, colName, start, $"LEN({colName})-{start - 1}");
                    }
                    else
                    {
                        _sqlBuilder.AppendFormat("{0}({1},{2})", funcName, colName, start);
                    }
                }
            }
        }

        private void ResolveSelectForFunc(MethodCallExpression callExp, string funcName)
        {
            if (callExp.Arguments[0] is UnaryExpression unary && unary.Operand is LambdaExpression lambda)
            {
                var colName = _queryBody.GetColumnName(lambda.Body as MemberExpression, lambda);
                _sqlBuilder.AppendFormat("{0}({1})", funcName, colName);
            }
        }


        #endregion

        private void NotResolve(Expression exp)
        {
            if (exp == null)
                return;

            _sqlBuilder.Append("(");

            UnaryResolve(exp);

            _sqlBuilder.Append(" = 0)");
        }

        private void MemberInitResolve(Expression exp)
        {
            if (exp == null || !(exp is MemberInitExpression initExp) || !initExp.Bindings.Any())
                return;

            for (var i = 0; i < initExp.Bindings.Count; i++)
            {
                if (initExp.Bindings[i] is MemberAssignment assignment)
                {
                    var descriptor = _queryBody.JoinDescriptors.First(m => m.EntityDescriptor.EntityType == initExp.Type);
                    var col = descriptor.EntityDescriptor.Columns.FirstOrDefault(m => m.PropertyInfo.Name.Equals(assignment.Member.Name));
                    if (col != null)
                    {
                        if (_queryBody.JoinDescriptors.Count > 1)
                            _sqlBuilder.Append($"{_sqlAdapter.AppendQuote(descriptor.Alias)}.{_sqlAdapter.AppendQuote(col.Name)}");
                        else
                            _sqlBuilder.Append(_sqlAdapter.AppendQuote(col.Name));

                        _sqlBuilder.Append("=");

                        Resolve(assignment.Expression);

                        if (i < initExp.Bindings.Count - 1)
                            _sqlBuilder.Append(",");
                    }
                }
            }
        }

        private object DynamicInvoke(Expression exp)
        {
            var result = Expression.Lambda(exp).Compile().DynamicInvoke();
            if (exp.Type.IsEnum)
                return result.ToInt();

            return result ?? "";
        }

        private void AppendValue(object value)
        {
            if (value == null)
            {
                var len = _sqlBuilder.Length;
                if (_sqlBuilder[len - 1] == ' ' && _sqlBuilder[len - 2] == '>' && _sqlBuilder[len - 3] == '<')
                {
                    _sqlBuilder.Remove(len - 3, 3);
                    _sqlBuilder.Append("IS NOT NULL");
                }
                else if (_sqlBuilder[len - 1] == ' ' && _sqlBuilder[len - 2] == '=')
                {
                    _sqlBuilder.Remove(len - 2, 2);
                    _sqlBuilder.Append("IS NULL");
                }
                return;
            }

            var pName = _parameters.Add(value);
            _sqlBuilder.Append(_sqlAdapter.AppendParameter(pName));
        }
    }
}
