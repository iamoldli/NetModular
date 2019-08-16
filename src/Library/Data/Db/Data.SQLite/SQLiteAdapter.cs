using System;
using System.Text;
using Nm.Lib.Data.Abstractions.Enums;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Data.SQLite
{
    internal class SQLiteAdapter : SqlAdapterAbstract
    {
        public SQLiteAdapter(string database) : base(database)
        {
        }

        public override string Database => AppendQuote(_database) + ".";

        public override SqlDialect SqlDialect => SqlDialect.SQLite;

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
        public override string IdentitySql => "SELECT LAST_INSERT_ROWID() ID;";

        public override string FuncLength => "LENGTH";

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1}", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (!string.IsNullOrWhiteSpace(sort))
                sqlBuilder.AppendFormat(" ORDER BY {0}", sort);

            sqlBuilder.AppendFormat(" LIMIT {0} OFFSET {1};", take, skip);
            return sqlBuilder.ToString();
        }

        public override string GenerateFirstSql(string select, string table, string where, string sort)
        {
            return GeneratePagingSql(select, table, where, sort, 0, 1);
        }

        public override Guid GenerateSequentialGuid()
        {
            return GuidHelper.NewSequentialGuid(SequentialGuidType.SequentialAsString);
        }
    }
}
