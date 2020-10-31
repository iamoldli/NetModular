using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.Options;
using NetModular.Lib.Data.Core;
using NetModular.Lib.Utils.Core.Helpers;

namespace NetModular.Lib.Data.MySql
{
    internal class MySqlAdapter : SqlAdapterAbstract
    {
        public MySqlAdapter(DbOptions dbOptions, DbModuleOptions options, ILoggerFactory loggerFactory) : base(dbOptions, options, loggerFactory?.CreateLogger<MySqlAdapter>())
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
        public override string ConnectionStringBuild(string tableName = null)
        {
            if (tableName.IsNull() && Options.ConnectionString.NotNull())
                return Options.ConnectionString;

            Check.NotNull(DbOptions.Server, nameof(DbOptions.Server), "数据库服务器地址不能为空");
            Check.NotNull(DbOptions.UserId, nameof(DbOptions.UserId), "数据库用户名不能为空");
            Check.NotNull(DbOptions.Password, nameof(DbOptions.Password), "数据库密码不能为空");

            Options.Version = DbOptions.Version;

            #region ==字符编码==

            var characterSet = "utf8";
            if (Options.MySqlCharacterSet.NotNull())
            {
                characterSet = Options.MySqlCharacterSet;
            }
            else if (DbOptions.MySqlCharacterSet.NotNull())
            {
                characterSet = DbOptions.MySqlCharacterSet;
            }

            #endregion

            #region ==SslMode==

            var sslModeStr = "None";
            if (Options.MySqlSslMode.NotNull())
            {
                sslModeStr = Options.MySqlSslMode;
            }
            else if (DbOptions.MySqlSslMode.NotNull())
            {
                sslModeStr = DbOptions.MySqlSslMode;
            }

            var sslModel = (MySqlSslMode)Enum.Parse(typeof(MySqlSslMode), sslModeStr);

            #endregion

            var connStrBuilder = new MySqlConnectionStringBuilder
            {
                Server = DbOptions.Server,
                Port = DbOptions.Port > 0 ? (uint)DbOptions.Port : 3306,
                Database = tableName.IsNull() ? Options.Database : tableName,
                UserID = DbOptions.UserId,
                Password = DbOptions.Password,
                AllowUserVariables = true,
                CharacterSet = characterSet,
                SslMode = sslModel,
                AllowPublicKeyRetrieval = true,
                MinimumPoolSize = DbOptions.MinPoolSize < 1 ? 0u : DbOptions.MinPoolSize.ToByte(),
                MaximumPoolSize = DbOptions.MaxPoolSize < 1 ? 10u : DbOptions.MaxPoolSize.ToByte()
            };

            //该参数为null表示使用的是当前模块的数据库
            if (tableName.IsNull())
                Options.ConnectionString = connStrBuilder.ToString();

            return Options.ConnectionString;
        }

        public override string GeneratePagingSql(string select, string table, string where, string sort, int skip, int take, string groupBy = null, string having = null)
        {
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("SELECT {0} FROM {1}", select, table);
            if (where.NotNull())
                sqlBuilder.AppendFormat(" WHERE {0}", where);

            if (groupBy.NotNull())
                sqlBuilder.Append(groupBy);

            if (having.NotNull())
                sqlBuilder.Append(having);

            if (sort.NotNull())
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
            return GuidHelper.NewSequentialGuid(SequentialGuidType.SequentialAsString);
        }

        public override void CreateDatabase(List<IEntityDescriptor> entityDescriptors, IDatabaseCreateEvents events, out bool databaseExists)
        {
            using var con = new MySqlConnection(ConnectionStringBuild("mysql"));
            con.Open();
            var cmd = con.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;

            //判断数据库是否已存在
            cmd.CommandText = $"SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{Options.Database}' LIMIT 1;";
            databaseExists = cmd.ExecuteScalar().ToInt() > 0;
            if (!databaseExists)
            {
                //执行创建前事件
                events?.Before().GetAwaiter().GetResult();

                //创建数据库
                cmd.CommandText = $"CREATE DATABASE {Options.Database} CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;";
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = $"USE `{Options.Database}`;";
            cmd.ExecuteNonQuery();

            //创建表
            foreach (var entityDescriptor in entityDescriptors)
            {
                if (!entityDescriptor.Ignore)
                {
                    var sql = GetCreateTableSql(entityDescriptor);
                    Logger?.LogInformation("执行创建表SQL：{@sql}", sql);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
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
                    throw new Exception("Property2Column error");
            }

            if (propertyType.IsEnum)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }

                return "SMALLINT(3)";
            }

            if (propertyType.IsGuid())
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
                    defaultValue = "DEFAULT 0";
                }
                return "BIT";
            }

            if (typeCode == TypeCode.Byte)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }
                return "TINYINT(1)";
            }

            if (typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }
                return "INT";
            }

            if (typeCode == TypeCode.Int64)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }
                return "BIGINT";
            }

            if (typeCode == TypeCode.DateTime)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT CURRENT_TIMESTAMP(0)";
                }
                return "DATETIME(0)";
            }

            if (typeCode == TypeCode.Decimal)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DECIMAL({m},{d})";
            }

            if (typeCode == TypeCode.Double)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DOUBLE({m},{d})";
            }

            if (typeCode == TypeCode.Single)
            {
                if (!isNullable)
                {
                    defaultValue = "DEFAULT 0";
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
            sql.AppendFormat("CREATE TABLE IF NOT EXISTS {0}(", AppendQuote(tableName ?? entityDescriptor.TableName));

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
                        sql.Append("AUTO_INCREMENT ");
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

            sql.Append(") ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_general_ci ROW_FORMAT = Dynamic;");

            return sql.ToString();
        }
    }
}
