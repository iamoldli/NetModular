using System;
using System.Collections.Generic;
using System.Text;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Data.Oracle
{
    internal class OracleAdapter : SqlAdapterAbstract
    {
        public OracleAdapter(DbOptions dbOptions, DbModuleOptions options) : base(dbOptions, options)
        {
        }

        public override string Database => AppendQuote(Options.Database) + ".";

        public override SqlDialect SqlDialect => SqlDialect.Oracle;

        public override char ParameterPrefix => ':';

        /// <summary>
        /// 左引号
        /// </summary>
        public override char LeftQuote => '`';

        /// <summary>
        /// 右引号
        /// </summary>
        public override char RightQuote => '`';

        /// <summary>
        /// 获取最后新增ID语句
        /// </summary>
        public override string IdentitySql => "";

        public override string FuncLength => "CHAR_LENGTH";

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take, string groupBy = null, string having = null)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1}", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (groupBy.NotNull())
                sqlBuilder.Append(groupBy);

            if (having.NotNull())
                sqlBuilder.Append(having);

            if (!string.IsNullOrWhiteSpace(sort))
                sqlBuilder.AppendFormat(" ORDER BY {0}", sort);

            sqlBuilder.AppendFormat(" LIMIT {0},{1}", skip, take);
            return sqlBuilder.ToString();
        }

        public override string GenerateFirstSql(string select, string table, string where, string sort, string groupBy = null, string having = null)
        {
            return GeneratePagingSql(select, table, where, sort, 0, 1, groupBy, having);
        }

        public override Guid GenerateSequentialGuid()
        {
            return GuidHelper.NewSequentialGuid(SequentialGuidType.SequentialAsBinary);
        }

        public override void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events, out bool databaseExists)
        {
            throw new NotImplementedException();
        }
    }
}
