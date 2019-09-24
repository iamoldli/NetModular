using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Abstractions.Entities;
using Nm.Lib.Data.Abstractions.Options;
using Nm.Lib.Data.Core;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Lib.Utils.Core.Helpers;

namespace Nm.Lib.Data.SqlServer
{
    public class SqlServerAdapter : SqlAdapterAbstract
    {
        public SqlServerAdapter(DbOptions dbOptions, DbModuleOptions options) : base(dbOptions, options)
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

                sqlBuilder.AppendFormat(") AS T WHERE T.RowNum BETWEEN {0} AND {1}", skip, skip + take);

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

        public override void CreateDatabase(EntityDescriptorCollection entityDescriptors)
        {
            var connStr = $"Server={DbOptions.Server};Database=master;Uid={DbOptions.UserId};Pwd={DbOptions.Password};MultipleActiveResultSets=true;";
            using (var con = new SqlConnection(connStr))
            {
                con.Open();

                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"IF NOT EXISTS (SELECT * FROM sysdatabases WHERE name = '{Options.Database}') CREATE DATABASE [{Options.Database}]";
                cmd.ExecuteNonQuery();

                cmd.CommandText = $"USE [{Options.Database}];";
                cmd.ExecuteNonQuery();

                foreach (var entityDescriptor in entityDescriptors)
                {
                    if (!entityDescriptor.Ignore)
                    {
                        cmd.CommandText = $"SELECT TOP 1 1 FROM sysobjects WHERE id = OBJECT_ID(N'{entityDescriptor.TableName}') AND xtype = 'U';";
                        var obj = cmd.ExecuteScalar();
                        if (obj.ToInt() < 1)
                        {
                            cmd.CommandText = CreateTableSql(entityDescriptor);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private string CreateTableSql(IEntityDescriptor entityDescriptor)
        {
            var columns = entityDescriptor.Columns;
            var sql = new StringBuilder();
            sql.AppendFormat("CREATE TABLE [{0}](", entityDescriptor.TableName);

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                sql.AppendFormat("[{0}] ", column.Name);
                sql.AppendFormat("{0} ", Property2Column(column, out string def));

                if (column.IsPrimaryKey)
                {
                    sql.Append("PRIMARY KEY ");

                    if (entityDescriptor.PrimaryKey.IsInt() || entityDescriptor.PrimaryKey.IsLong())
                    {
                        sql.Append("IDENTITY(1,1) ");
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

            sql.Append(");");

            return sql.ToString();
        }

        /// <summary>
        /// 属性转换为列
        /// </summary>
        /// <param name="propertyInfo"></param>
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

                return "SMALLINT";
            }

            if (propertyType == typeof(Guid))
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
                    def = "DEFAULT(GETDATE())";
                }
                return "DATETIME";
            }

            if (typeCode == TypeCode.Decimal || typeCode == TypeCode.Double)
            {
                if (!isNullable)
                {
                    def = "DEFAULT(0)";
                }

                var m = column.PrecisionM < 1 ? 18 : column.PrecisionM;
                var d = column.PrecisionD < 1 ? 4 : column.PrecisionD;

                return $"DECIMAL({m},{d})";
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
