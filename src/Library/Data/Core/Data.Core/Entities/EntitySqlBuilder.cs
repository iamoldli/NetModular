using System.Collections.Generic;
using System.Linq;
using System.Text;
using Td.Lib.Data.Abstractions.Entities;

namespace Td.Lib.Data.Core.Entities
{
    internal class EntitySqlBuilder : IEntitySqlBuilder
    {
        public EntitySql Build(IEntityDescriptor descriptor)
        {
            var batchInsertColumnList = new List<IColumnDescriptor>();
            var insertSql = BuildInsertSql(descriptor, batchInsertColumnList, out string batchInsertSql);
            var deleteSql = BuildDeleteSql(descriptor, out string deleteSingleSql);
            var softDeleteSql = BuildSoftDeleteSql(descriptor, out string softDeleteSingleSql);
            var updateSql = BuildUpdateSql(descriptor, out string updateSingleSql);
            var querySql = BuildQuerySql(descriptor, out string getSql);
            var existsSql = BuildExistsSql(descriptor);
            return new EntitySql
            {
                Insert = insertSql,
                BatchInsert = batchInsertSql,
                BatchInsertColumnList = batchInsertColumnList,
                Delete = deleteSql,
                DeleteSingle = deleteSingleSql,
                SoftDelete = softDeleteSql,
                SoftDeleteSingle = softDeleteSingleSql,
                Update = updateSql,
                UpdateSingle = updateSingleSql,
                Query = querySql,
                Get = getSql,
                Exists = existsSql
            };
        }

        #region ==Private Methods==

        /// <summary>
        /// 设置插入语句
        /// </summary>
        private string BuildInsertSql(IEntityDescriptor descriptor, List<IColumnDescriptor> batchInsertColumnList, out string batchInsertSql)
        {
            var sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            descriptor.SqlAdapter.AppendQuote(sb, descriptor.TableName);
            sb.Append("(");

            var valuesSql = new StringBuilder();

            foreach (var col in descriptor.Columns)
            {
                //排除自增主键
                if (col.IsPrimaryKey && (descriptor.PrimaryKey.IsInt() || descriptor.PrimaryKey.IsLong()))
                    continue;

                descriptor.SqlAdapter.AppendQuote(sb, col.Name);
                sb.Append(",");

                descriptor.SqlAdapter.AppendParameter(valuesSql, col.PropertyInfo.Name);
                valuesSql.Append(",");

                batchInsertColumnList.Add(col);
            }

            //删除最后一个","
            sb.Remove(sb.Length - 1, 1);

            sb.Append(") VALUES");

            batchInsertSql = sb.ToString();

            sb.Append("(");

            //删除最后一个","
            valuesSql.Remove(valuesSql.Length - 1, 1);
            sb.Append(valuesSql);
            sb.Append(");");

            return sb.ToString();
        }

        /// <summary>
        /// 设置删除语句
        /// </summary>
        private string BuildDeleteSql(IEntityDescriptor descriptor, out string deleteSingleSql)
        {
            var deleteSql = $"DELETE FROM {descriptor.SqlAdapter.AppendQuote(descriptor.TableName)} ";
            if (!descriptor.PrimaryKey.IsNo())
                deleteSingleSql = $"{deleteSql} WHERE {descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name)}={descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name)};";
            else
                deleteSingleSql = "";

            return deleteSql;
        }

        /// <summary>
        /// 设置软删除
        /// </summary>
        private string BuildSoftDeleteSql(IEntityDescriptor descriptor, out string softDeleteSingleSql)
        {
            if (!descriptor.SoftDelete)
            {
                softDeleteSingleSql = string.Empty;
                return string.Empty;
            }

            var sb = new StringBuilder($"UPDATE {descriptor.SqlAdapter.AppendQuote(descriptor.TableName)} SET ");
            sb.AppendFormat("{0}=1,", descriptor.SqlAdapter.AppendQuote("Deleted"));
            sb.AppendFormat("{0}={1},", descriptor.SqlAdapter.AppendQuote("DeletedTime"), descriptor.SqlAdapter.AppendParameter("DeletedTime"));
            sb.AppendFormat("{0}={1} ", descriptor.SqlAdapter.AppendQuote("DeletedBy"), descriptor.SqlAdapter.AppendParameter("DeletedBy"));

            var softDeleteSql = sb.ToString();

            sb.AppendFormat(" WHERE {0}={1};", descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name), descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name));
            softDeleteSingleSql = sb.ToString();

            return softDeleteSql;
        }

        /// <summary>
        /// 设置更新语句
        /// </summary>
        private string BuildUpdateSql(IEntityDescriptor descriptor, out string updateSingleSql)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("UPDATE ");
            descriptor.SqlAdapter.AppendQuote(sb, descriptor.TableName);
            sb.Append(" SET ");

            var updateSql = sb.ToString();
            updateSingleSql = "";
            if (!descriptor.PrimaryKey.IsNo())
            {
                var columns = descriptor.Columns.Where(m => !m.IsPrimaryKey);

                foreach (var col in columns)
                {
                    sb.AppendFormat("{0}={1}", descriptor.SqlAdapter.AppendQuote(col.Name), descriptor.SqlAdapter.AppendParameter(col.PropertyInfo.Name));
                    sb.Append(",");
                }

                sb.Remove(sb.Length - 1, 1);

                sb.AppendFormat(" WHERE {0}={1};", descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name), descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name));

                updateSingleSql = sb.ToString();
            }

            return updateSql;
        }

        /// <summary>
        /// 设置查询语句
        /// </summary>
        private string BuildQuerySql(IEntityDescriptor descriptor, out string getSql)
        {
            var sb = new StringBuilder("SELECT ");
            for (var i = 0; i < descriptor.Columns.Count; i++)
            {
                var col = descriptor.Columns[i];
                sb.AppendFormat("{0} AS '{1}'", descriptor.SqlAdapter.AppendQuote(col.Name), col.PropertyInfo.Name);

                if (i != descriptor.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(" FROM ");
            descriptor.SqlAdapter.AppendQuote(sb, descriptor.TableName);

            var querySql = sb.ToString();
            getSql = "";
            if (!descriptor.PrimaryKey.IsNo())
            {
                sb.AppendFormat(" WHERE {0}={1};", descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name), descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name));
                getSql = sb.ToString();
            }

            return querySql;
        }

        /// <summary>
        /// 设置是否存在语句
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        private string BuildExistsSql(IEntityDescriptor descriptor)
        {
            //没有主键，无法使用该方法
            if (descriptor.PrimaryKey.IsNo())
                return string.Empty;

            return $"SELECT COUNT(0) FROM {descriptor.SqlAdapter.AppendQuote(descriptor.TableName)} WHERE {descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name)}={descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name)};";
        }
        #endregion
    }
}
