using System;
using System.Text;
using MySql.Data.MySqlClient;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.Enums;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Data.MySql
{
    internal class MySqlAdapter : SqlAdapterAbstract
    {
        public MySqlAdapter(DbOptions dbOptions, DbModuleOptions options) : base(dbOptions, options)
        {
        }

        public override string Database => AppendQuote(Options.Database) + ".";

        public override SqlDialect SqlDialect => SqlDialect.MySql;

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
        public override string IdentitySql => "SELECT LAST_INSERT_ID() ID;";

        public override string FuncLength => "CHAR_LENGTH";

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1}", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (!string.IsNullOrWhiteSpace(sort))
                sqlBuilder.AppendFormat(" ORDER BY {0}", sort);

            sqlBuilder.AppendFormat(" LIMIT {0},{1}", skip, take);
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

        public override void CreateDatabase(EntityDescriptorCollection entityDescriptors)
        {
            var connStr = $"Server={DbOptions.Server};Database=mysql;Uid={DbOptions.UserId};Pwd={DbOptions.Password};Allow User Variables=True;charset=utf8;SslMode=none;";
            using (var con = new MySqlConnection(connStr))
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS {Options.Database} CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"USE `{Options.Database}`;";
                cmd.ExecuteNonQuery();

                foreach (var entityDescriptor in entityDescriptors)
                {
                    if (!entityDescriptor.Ignore)
                    {
                        cmd.CommandText = CreateTableSql(entityDescriptor);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private string CreateTableSql(IEntityDescriptor entityDescriptor)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE IF NOT EXISTS {0}(", AppendQuote(entityDescriptor.TableName));

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("`{0}` ", column.Name);
                sql.AppendFormat("{0} ", Property2Column(column, out string def));

                if (column.IsPrimaryKey)
                {
                    sql.Append("PRIMARY KEY ");

                    if (entityDescriptor.PrimaryKey.IsInt() || entityDescriptor.PrimaryKey.IsLong())
                    {
                        sql.Append("AUTO_INCREMENT ");
                    }

                    def = string.Empty;
                }

                if (!column.Nullable)
                {
                    sql.Append("NOT NULL ");
                }

                if (def.NotNull())
                {
                    sql.Append(def);
                }

                if (i < columns.Count - 1)
                {
                    sql.Append(",");
                }
            }

            sql.Append(") ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;");

            return sql.ToString();
        }

        /// <summary>
        /// 属性转换为列
        /// </summary>
        /// <param name="column"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public string Property2Column(IColumnDescriptor column, out string def)
        {
            def = "";
            var propertyType = column.PropertyInfo.PropertyType;
            var isNullable = propertyType.IsNullable();
            if (isNullable)
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
                if (propertyType == null)
                    throw new Exception("Property2Column error");
            }

            if (propertyType.IsEnum)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }

                return "SMALLINT(3)";
            }

            if (propertyType == typeof(Guid))
                return "CHAR(36)";

            var typeCode = Type.GetTypeCode(propertyType);
            if (typeCode == TypeCode.Char || typeCode == TypeCode.String)
            {
                if (column.Max)
                    return "TEXT";

                if (column.Length < 1)
                    return "VARCHAR(50)";

                return $"VARCHAR({column.Length})";
            }

            if (typeCode == TypeCode.Boolean)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }
                return "BIT";
            }

            if (typeCode == TypeCode.Byte)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }
                return "TINYINT(1)";
            }

            if (typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }
                return "INT";
            }

            if (typeCode == TypeCode.Int64)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }
                return "BIGINT";
            }

            if (typeCode == TypeCode.DateTime)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(NOW())";
                }
                return "DATETIME(0)";
            }

            if (typeCode == TypeCode.Decimal)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DECIMAL({m},{d})";
            }

            if (typeCode == TypeCode.Double)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DOUBLE({m},{d})";
            }

            if (typeCode == TypeCode.Single)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"FLOAT({m},{d})";
            }

            return string.Empty;
        }
    }
}
