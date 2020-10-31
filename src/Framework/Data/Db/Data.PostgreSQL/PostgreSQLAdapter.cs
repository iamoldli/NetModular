using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Helpers;
using Npgsql;

namespace NetModular.Lib.Data.PostgreSQL
{
    internal class PostgreSQLAdapter : SqlAdapterAbstract
    {
        public PostgreSQLAdapter(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory) : base(dbOptions, options, loggerFactory?.CreateLogger<PostgreSQLAdapter>())
        {
        }

        public override string Database => AppendQuote(Options.Database) + ".";

        public override SqlDialect SqlDialect => SqlDialect.PostgreSQL;

        /// <summary>
        /// 获取最后新增ID语句
        /// </summary>
        public override string IdentitySql => "RETURNING \"id\";";

        public override string FuncLength => "CHAR_LENGTH";

        public override bool ToLower => true;
        public override string ConnectionStringBuild(string tableName = null)
        {
            if (tableName.NotNull() || Options.ConnectionString.IsNull())
            {

                Check.NotNull(DbOptions.Server, nameof(DbOptions.Server), "数据库服务器地址不能为空");
                Check.NotNull(DbOptions.UserId, nameof(DbOptions.UserId), "数据库用户名不能为空");
                Check.NotNull(DbOptions.Password, nameof(DbOptions.Password), "数据库密码不能为空");

                Options.Version = DbOptions.Version;
                var connStrBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = DbOptions.Server,
                    Port = DbOptions.Port > 0 ? DbOptions.Port : 5432,
                    Database = tableName.NotNull() ? tableName : Options.Database,
                    Username = DbOptions.UserId,
                    Password = DbOptions.Password
                };
                if (DbOptions.NpgsqlDatabaseName.NotNull())
                {
                    connStrBuilder.Database = DbOptions.NpgsqlDatabaseName;
                }

                //该参数为null表示使用的是当前模块的数据库
                if (tableName.IsNull())
                    Options.ConnectionString = connStrBuilder.ToString();
            }

            return Options.ConnectionString;
        }

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take, string groupBy = null, string having = null)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1} ", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat("WHERE {0} ", where);

            if (groupBy.NotNull())
                sqlBuilder.Append(groupBy);

            if (having.NotNull())
                sqlBuilder.Append(having);

            if (!string.IsNullOrWhiteSpace(sort))
                sqlBuilder.AppendFormat("ORDER BY {0} ", sort);

            sqlBuilder.AppendFormat("LIMIT {0} ", take);
            if (skip > 0)
            {
                sqlBuilder.AppendFormat("OFFSET {0} ", skip);
            }
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
            if (DbOptions.NpgsqlDatabaseName.NotNull())
            {
                using var con1 = new NpgsqlConnection(ConnectionStringBuild("postgres"));
                con1.Open();
                var existsDatabase = con1.ExecuteScalar($"SELECT 1 FROM pg_catalog.pg_database u where u.datname='{DbOptions.NpgsqlDatabaseName}';").ToInt() > 0;
                if (!existsDatabase)
                {
                    //创建数据库
                    con1.Execute($"CREATE DATABASE {DbOptions.NpgsqlDatabaseName};");
                }
                con1.Close();
            }

            using var con = new NpgsqlConnection(Options.ConnectionString);
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //判断数据库是否已存在
            cmd.CommandText = $"SELECT 1 FROM pg_namespace WHERE nspname = '{Options.Database}' LIMIT 1;";
            databaseExists = cmd.ExecuteScalar().ToInt() > 0;
            if (!databaseExists)
            {
                //执行创建前事件
                events?.Before().GetAwaiter().GetResult();

                //创建数据库
                cmd.CommandText = $"CREATE SCHEMA {Options.Database};";
                cmd.ExecuteNonQuery();
            }

            //创建表
            foreach (var entityDescriptor in entityDescriptors)
            {
                if (!entityDescriptor.Ignore)
                {
                    var sql = GetCreateTableSql(entityDescriptor);
                    Logger?.LogInformation("执行创建表SQL：{@sql}", sql);
                    cmd.CommandText = sql;
                    con.Execute(GetCreateTableSql(entityDescriptor));
                }
            }

            if (!databaseExists)
            {
                //执行创建后事件
                events?.After().GetAwaiter().GetResult();
            }

            con.Close();
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
                    throw new Exception("ResolveColumnTypeName error");
            }

            if (propertyType.IsEnum)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }

                return "SMALLINT";
            }
            if (propertyType.IsGuid())
            {
                return "UUID";
            }
            var typeCode = Type.GetTypeCode(propertyType);
            switch (typeCode)
            {
                case TypeCode.String:
                    if (column.Max)
                        return "TEXT";

                    if (column.Length < 1)
                        return "VARCHAR(50)";

                    return $"VARCHAR({column.Length})";
                case TypeCode.Char:
                    column.TypeName = $"CHAR({column.Length})";
                    break;
                case TypeCode.Boolean:
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT FALSE";
                    }
                    return "boolean";
                case TypeCode.Byte:
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT 0";
                    }
                    return "SMALLINT";
                case TypeCode.Int16:
                    if (column.IsPrimaryKey)
                    {
                        return "SMALLSERIAL";
                    }

                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT 0";
                    }
                    return "SMALLINT";
                case TypeCode.Int32:
                    if (column.IsPrimaryKey)
                    {
                        return "SERIAL";
                    }
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT 0";
                    }

                    return "INTEGER";
                case TypeCode.Int64:
                    if (column.IsPrimaryKey)
                    {
                        return "BIGSERIAL";
                    }
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT 0";
                    }

                    return "BIGINT";
                case TypeCode.DateTime:
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT CURRENT_TIMESTAMP";
                    }

                    return "TIMESTAMP";
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    if (!isNullable)
                    {
                        defaultValue = "DEFAULT 0";
                    }
                    return "MONEY";
            }

            return string.Empty;
        }

        public override string GetCreateTableSql(IEntityDescriptor entityDescriptor, string tableName = null)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE IF NOT EXISTS {0}.{1}(", AppendQuote(Options.Database), AppendQuote(tableName ?? entityDescriptor.TableName.ToLower()));

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("{0} ", AppendQuote(column.Name.ToLower()));
                sql.AppendFormat("{0} ", column.TypeName);

                if (column.IsPrimaryKey)
                {
                    sql.Append("PRIMARY KEY ");
                }

                if (!column.Nullable && !column.IsPrimaryKey)
                {
                    sql.Append("NOT NULL ");
                }

                if (!column.IsPrimaryKey && column.DefaultValue.NotNull())
                {
                    sql.Append(column.DefaultValue);
                }

                if (i < columns.Count - 1)
                {
                    sql.Append(",");
                }
            }

            sql.Append(");");

            return sql.ToString();
        }
    }
}
