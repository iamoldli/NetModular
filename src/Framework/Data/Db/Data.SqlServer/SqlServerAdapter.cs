using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Data.SqlServer
{
    public class SqlServerAdapter : SqlAdapterAbstract
    {
        public SqlServerAdapter(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory) : base(dbOptions, options, loggerFactory?.CreateLogger<SqlServerAdapter>())
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
        public override string ConnectionStringBuild(string tableName = null)
        {
            if (Options.ConnectionString.IsNull())
            {
                Check.NotNull(DbOptions.Server, nameof(DbOptions.Server), "数据库服务器地址不能为空");
                Check.NotNull(DbOptions.UserId, nameof(DbOptions.UserId), "数据库用户名不能为空");
                Check.NotNull(DbOptions.Password, nameof(DbOptions.Password), "数据库密码不能为空");

                Options.Version = DbOptions.Version;
                var connStrBuilder = new SqlConnectionStringBuilder
                {
                    DataSource = DbOptions.Port > 0 ? DbOptions.Server + "," + DbOptions.Port : DbOptions.Server,
                    UserID = DbOptions.UserId,
                    Password = DbOptions.Password,
                    MultipleActiveResultSets = true,
                    InitialCatalog = tableName.NotNull() ? tableName : Options.Database,
                    MaxPoolSize = DbOptions.MaxPoolSize < 1 ? 100 : DbOptions.MaxPoolSize,
                    MinPoolSize = DbOptions.MinPoolSize < 1 ? 0 : DbOptions.MinPoolSize
                };
                Options.ConnectionString = connStrBuilder.ToString();
            }

            return Options.ConnectionString;
        }

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take, string groupBy = null, string having = null)
        {
            var sqlBuilder = new StringBuilder();

            if (Options.Version.IsNull() || Options.Version.ToInt() >= 2012)
            {
                #region ==2012+版本==

                sqlBuilder.AppendFormat("SELECT {0} FROM {1}", @select, table);
                if (!string.IsNullOrWhiteSpace(where))
                    sqlBuilder.AppendFormat(" WHERE {0}", @where);

                if (groupBy.NotNull())
                    sqlBuilder.Append(groupBy);

                if (having.NotNull())
                    sqlBuilder.Append(having);

                sqlBuilder.AppendFormat(" ORDER BY {0} OFFSET {1} ROW FETCH NEXT {2} ROW ONLY", sort, skip, take);

                #endregion
            }
            else
            {
                #region ==2018及以下版本==

                sqlBuilder.AppendFormat("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNum,{1} FROM {2}", sort, @select, table);
                if (!string.IsNullOrWhiteSpace(where))
                    sqlBuilder.AppendFormat(" WHERE {0}", @where);

                sqlBuilder.AppendFormat(") AS T WHERE T.RowNum BETWEEN {0} AND {1}", skip, skip + take);

                #endregion
            }

            return sqlBuilder.ToString();
        }

        public override string GenerateFirstSql(string select, string table, string where, string sort, string groupBy = null, string having = null)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT TOP 1 {0} FROM {1}", select, table);
            if (!string.IsNullOrWhiteSpace(where))
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (groupBy.NotNull())
                sqlBuilder.Append(groupBy);

            if (having.NotNull())
                sqlBuilder.Append(having);

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

        public override void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events, out bool databaseExists)
        {
            using var con = new SqlConnection(ConnectionStringBuild("master"));
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //判断数据库是否已存在
            cmd.CommandText = $"SELECT TOP 1 1 FROM sysdatabases WHERE name = '{Options.Database}'";
            databaseExists = cmd.ExecuteScalar().ToInt() > 0;
            if (!databaseExists)
            {
                //执行创建前事件
                events?.Before().GetAwaiter().GetResult();

                //创建数据库
                cmd.CommandText = $"CREATE DATABASE [{Options.Database}]";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = $"USE [{Options.Database}];";
            cmd.ExecuteNonQuery();

            //创建表
            foreach (var entityDescriptor in entityDescriptors)
            {
                if (!entityDescriptor.Ignore)
                {
                    cmd.CommandText = $"SELECT TOP 1 1 FROM sysobjects WHERE id = OBJECT_ID(N'{entityDescriptor.TableName}') AND xtype = 'U';";
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
                //执行创建后事件
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
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }

                return "SMALLINT";
            }

            if (propertyType.IsGuid())
                return "UNIQUEIDENTIFIER";

            var typeCode = Type.GetTypeCode(propertyType);
            if (typeCode == TypeCode.Char || typeCode == TypeCode.String)
            {
                if (column.Max)
                    return "NVARCHAR(MAX)";

                if (column.Length < 1)
                    return "NVARCHAR(50)";

                return $"NVARCHAR({column.Length})";
            }

            if (typeCode == TypeCode.Boolean)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }
                return "BIT";
            }

            if (typeCode == TypeCode.Byte)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }
                return "TINYINT(1)";
            }

            if (typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }
                return "INT";
            }

            if (typeCode == TypeCode.Int64)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }
                return "BIGINT";
            }

            if (typeCode == TypeCode.DateTime)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(GETDATE())";
                }
                return "DATETIME";
            }

            if (typeCode == TypeCode.Decimal || typeCode == TypeCode.Double)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DECIMAL({m},{d})";
            }

            if (typeCode == TypeCode.Single)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"FLOAT({m},{d})";
            }

            return string.Empty;
        }

        public override string GetCreateTableSql(IEntityDescriptor entityDescriptor, string tableName = null)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE [{0}](", tableName ?? entityDescriptor.TableName);

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("[{0}] ", column.Name);
                sql.AppendFormat("{0} ", column.TypeName);

                if (column.IsPrimaryKey)
                {
                    sql.Append("PRIMARY KEY ");

                    if (entityDescriptor.PrimaryKey.IsInt() || entityDescriptor.PrimaryKey.IsLong())
                    {
                        sql.Append("IDENTITY(1,1) ");
                    }
                }

                if (!column.Nullable)
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
