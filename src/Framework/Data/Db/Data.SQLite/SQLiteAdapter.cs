using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Data.SQLite
{
    internal class SQLiteAdapter : SqlAdapterAbstract
    {
        public SQLiteAdapter(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory) : base(dbOptions, options, loggerFactory?.CreateLogger<SQLiteAdapter>())
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

            sqlBuilder.AppendFormat(" LIMIT {0} OFFSET {1};", take, skip);
            return sqlBuilder.ToString();
        }

        public override string GenerateFirstSql(string select, string table, string where, string sort, string groupBy = null, string having = null)
        {
            return GeneratePagingSql(select, table, where, sort, 0, 1, groupBy, having);
        }

        public override Guid GenerateSequentialGuid()
        {
            return GuidHelper.NewSequentialGuid(SequentialGuidType.SequentialAsString);
        }

        public override void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events, out bool databaseExists)
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
            databaseExists = File.Exists(dbFilePath);

            if (!databaseExists)
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
                    cmd.CommandText = $"SELECT 1 FROM sqlite_master WHERE type = 'table' and name='{entityDescriptor.TableName}';";
                    var obj = cmd.ExecuteScalar();
                    if (obj.ToInt() < 1)
                    {
                        var sql = GetCreateTableSql(entityDescriptor);
                        Logger?.LogInformation("执行创建表SQL：{@sql}", sql);
                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            if (!databaseExists)
            {
                //执行创建前事件
                events?.After().GetAwaiter().GetResult();
            }
        }

        public override string GetColumnTypeName(IColumnDescriptor column, out string defaultValue)
        {
            defaultValue = "";

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

            if (propertyType.IsGuid())
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

        public override string GetCreateTableSql(IEntityDescriptor entityDescriptor, string tableName = null)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE {0}(", AppendQuote(tableName ?? entityDescriptor.TableName));

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("`{0}` ", column.Name);
                sql.AppendFormat("{0} ", column.TypeName);

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
    }
}
