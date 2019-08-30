using System;
using System.Text;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Data.SqlServer
{
    public class SqlServerAdapter : SqlAdapterAbstract
    {
        public SqlServerAdapter(DbConnectionOptions options) : base(options)
        {
        }

        public override string Database => AppendQuote(Options.Database) + "..";

        /// <summary>
        /// 左引号
        /// </summary>
        public override char LeftQuote => '[';

        /// <summary>
        /// 右引号
        /// </summary>
        public override char RightQuote => ']';

        /// <summary>
        /// 获取最后新增ID语句
        /// </summary>
        public override string IdentitySql => "SELECT SCOPE_IDENTITY() ID;";

        public override string FuncSubstring => "SUBSTRING";

        public override string FuncLength => "LEN";

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take)
        {
            var sqlBuilder = new StringBuilder();

            if (Options.Version.IsNull() || Options.Version.ToInt() >= 2012)
            {
                #region ==2012+版本==

                sqlBuilder.AppendFormat("SELECT {0} FROM {1}", @select, table);
                if (!string.IsNullOrWhiteSpace(where))
                    sqlBuilder.AppendFormat(" WHERE {0}", @where);

                sqlBuilder.AppendFormat(" ORDER BY {0} OFFSET {1} ROW FETCH NEXT {2} ROW ONLY", sort, skip, take);

                #endregion
            }
            else
            {
                #region ==2018及以下版本==

                sqlBuilder.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNum,{1} FROM {2}", sort, @select, table);
                if (!string.IsNullOrWhiteSpace(where))
                    sqlBuilder.AppendFormat(" WHERE {0}", @where);

                sqlBuilder.AppendFormat(") AS T WHERE T.RowNum BETWEEN {0} AND {1}", skip, take);

                #endregion
            }

            return sqlBuilder.ToString();
        }

        public override string GenerateFirstSql(string select, string table, string where, string sort)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT TOP 1 {0} FROM {1}", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                sqlBuilder.AppendFormat(" ORDER BY {0} ", sort);
            }

            return sqlBuilder.ToString();
        }

        public override Guid GenerateSequentialGuid()
        {
            return GuidHelper.NewSequentialGuid(SequentialGuidType.SequentialAtEnd);
        }
    }
}
