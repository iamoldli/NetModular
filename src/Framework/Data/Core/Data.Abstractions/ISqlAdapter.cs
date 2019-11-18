using System;
using System.Collections.Generic;
using System.Text;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;

namespace NetModular.Lib.Data.Abstractions
{
    public interface ISqlAdapter
    {
        #region ==属性==

        /// <summary>
        /// 数据库配置项
        /// </summary>
        DbOptions DbOptions { get; }

        /// <summary>
        /// 数据库模块
        /// </summary>
        DbModuleOptions Options { get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        string Database { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        SqlDialect SqlDialect { get; }

        /// <summary>
        /// 左引号
        /// </summary>
        char LeftQuote { get; }

        /// <summary>
        /// 右引号
        /// </summary>
        char RightQuote { get; }

        /// <summary>
        /// 参数前缀符号
        /// </summary>
        char ParameterPrefix { get; }

        /// <summary>
        /// 获取新增ID的SQL语句
        /// </summary>
        string IdentitySql { get; }

        /// <summary>
        /// 字符串截取函数
        /// </summary>
        string FuncSubstring { get; }

        /// <summary>
        /// 字符串长度函数
        /// </summary>
        string FuncLength { get; }

        /// <summary>
        /// 转小写函数
        /// </summary>
        string FuncLower { get; }

        /// <summary>
        /// 转大写函数
        /// </summary>
        string FuncUpper { get; }

        /// <summary>
        /// 命名小写
        /// </summary>
        bool ToLower { get; }

        #endregion

        #region ==方法==

        /// <summary>
        /// 给定的值附加引号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string AppendQuote(string value);

        /// <summary>
        /// 给定的值附加引号
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        void AppendQuote(StringBuilder sb, string value);

        /// <summary>
        /// 附加参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        string AppendParameter(string parameterName);

        /// <summary>
        /// 附加参数
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        void AppendParameter(StringBuilder sb, string parameterName);

        /// <summary>
        /// 附加含有值的参数
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        string AppendParameterWithValue(string parameterName);

        /// <summary>
        /// 附加含有值的参数
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        void AppendParameterWithValue(StringBuilder sb, string parameterName);

        /// <summary>
        /// 附加查询条件
        /// </summary>
        /// <param name="queryWhere"></param>
        /// <returns></returns>
        string AppendQueryWhere(string queryWhere);

        /// <summary>
        /// 附加查询条件
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="queryWhere">查询条件</param>
        void AppendQueryWhere(StringBuilder sb, string queryWhere);

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="select"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take);

        /// <summary>
        /// 生成获取第一条数据的Sql
        /// </summary>
        /// <param name="select"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        string GenerateFirstSql(string select, string table, string where, string sort);

        /// <summary>
        /// 生成有序Guid
        /// </summary>
        /// <returns></returns>
        Guid GenerateSequentialGuid();

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="entityDescriptors">实体信息集合</param>
        /// <param name="events">创建事件</param>
        void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events = null);

        #endregion
    }
}
