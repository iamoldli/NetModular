using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Extensions;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Data.SQLite
{
    internal class SQLiteAdapter : SqlAdapterAbstract
    {
        public SQLiteAdapter(DbOptions dbOptions, DbModuleOptions options) : base(dbOptions, options)
        {
        }

        public override string Database => AppendQuote(Options.Database) + ".";

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

        public override void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events = null)
        {
            string dbFilePath = Path.Combine(AppContext.BaseDirectory, "Db");
            if (DbOptions.Server.NotNull())
            {
                dbFilePath = Path.GetFullPath(DbOptions.Server);
            }

            if (!Directory.Exists(dbFilePath))
            {
                Directory.CreateDirectory(dbFilePath);
            }

            dbFilePath = Path.Combine(dbFilePath, Options.Database) + ".db";

            //判断是否存在
            var exist = File.Exists(dbFilePath);
            if (!exist)
            {
                //执行创建前事件
                events?.Before().GetAwaiter().GetResult();
            }

            var connStrBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = $"{dbFilePath}",
                Mode = SqliteOpenMode.ReadWriteCreate
            };

            using var con = new SqliteConnection(connStrBuilder.ToString());
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            foreach (var entityDescriptor in entityDescriptors)
            {
                if (!entityDescriptor.Ignore)
                {
                    cmd.CommandText =
                        $"SELECT 1 FROM sqlite_master WHERE type = 'table' and name='{entityDescriptor.TableName}';";
                    var obj = cmd.ExecuteScalar();
                    if (obj.ToInt() < 1)
                    {
                        cmd.CommandText = CreateTableSql(entityDescriptor);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (!exist)
            {
                //执行创建前事件
                events?.After().GetAwaiter().GetResult();
            }
        }

        private string CreateTableSql(IEntityDescriptor entityDescriptor)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE {0}(", AppendQuote(entityDescriptor.TableName));

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("`{0}` ", column.Name);
                sql.AppendFormat("{0} ", Property2Column(column));

                if (column.IsPrimaryKey)
                {
                    sql.Append("PRIMARY KEY ");

                    if (entityDescriptor.PrimaryKey.IsInt() || entityDescriptor.PrimaryKey.IsLong())
                    {
                        sql.Append("AUTOINCREMENT ");
                    }
                }

                if (!column.Nullable)
                {
                    sql.Append("NOT NULL ");
                }

                if (i < columns.Count - 1)
                {
                    sql.Append(",");
                }
            }

            sql.Append(")");

            return sql.ToString();
        }

        /// <summary>
        /// 属性转换为列
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public string Property2Column(IColumnDescriptor column)
        {
            var propertyType = column.PropertyInfo.PropertyType;
            var isNullable = propertyType.IsNullable();
            if (isNullable)
            {
                propertyType = Nullable.GetUnderlyingType(propertyType);
                if (propertyType == null)
                    throw new Exception("Property2Column error");
            }

            if (propertyType.IsEnum)
                return "integer";

            if (propertyType == typeof(Guid))
                return "UNIQUEIDENTIFIER";

            var typeCode = Type.GetTypeCode(propertyType);
            if (typeCode == TypeCode.Char || typeCode == TypeCode.String)
                return "text";

            switch (typeCode)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return "integer";
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                    var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;
                    return $"DECIMAL({m},{d})";
                default:
                    return "text";
            }
        }
    }
}
