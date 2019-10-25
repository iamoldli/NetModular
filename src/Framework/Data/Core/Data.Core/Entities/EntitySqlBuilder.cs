using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core.Entities
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
            var querySql = BuildQuerySql(descriptor, out string getSql, out string getAdnRowLockSql);
            var existsSql = BuildExistsSql(descriptor);

            return new EntitySql(descriptor, insertSql, batchInsertSql, deleteSingleSql, deleteSql, softDeleteSql,
                softDeleteSingleSql, updateSingleSql, updateSql, getSql, getAdnRowLockSql, querySql, existsSql, batchInsertColumnList);
        }

        #region ==Private Methods==

        /// <summary>
        /// 设置插入语句
        /// </summary>
        private string BuildInsertSql(IEntityDescriptor descriptor, List<IColumnDescriptor> batchInsertColumnList, out string batchInsertSql)
        {
            var sb = new StringBuilder();
            sb.Append("INSERT INTO {0} ");
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
            if (valuesSql.Length > 0)
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
            var deleteSql = "DELETE FROM {0} ";
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

            var sb = new StringBuilder("UPDATE {0} SET ");
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
            sb.Append("UPDATE {0} SET");

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
        private string BuildQuerySql(IEntityDescriptor descriptor, out string getSql, out string getAndRowLockSql)
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
            sb.Append(" FROM {0} ");

            var querySql = sb.ToString();
            getSql = querySql;
            getAndRowLockSql = querySql;
            // SqlServer行锁
            if (descriptor.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SqlServer)
            {
                getAndRowLockSql += " WITH (ROWLOCK, UPDLOCK) ";
            }

            if (!descriptor.PrimaryKey.IsNo())
            {
                getSql += $" WHERE {descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name)}={descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name)} ";
                getAndRowLockSql += $" WHERE {descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name)}={descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name)} ";

                if (descriptor.SoftDelete)
                {
                    getSql += $" AND {descriptor.SqlAdapter.AppendQuote("Deleted")}=0 ";
                    getAndRowLockSql += $" AND {descriptor.SqlAdapter.AppendQuote("Deleted")}=0 ";
                }

                //MySql行锁
                if (descriptor.SqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.MySql)
                {
                    getAndRowLockSql += " FOR UPDATE;";
                }
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

            var sql = $"SELECT COUNT(0) FROM {{0}} WHERE {descriptor.SqlAdapter.AppendQuote(descriptor.PrimaryKey.Name)}={descriptor.SqlAdapter.AppendParameter(descriptor.PrimaryKey.PropertyInfo.Name)}";
            if (descriptor.SoftDelete)
            {
                sql += $" AND {descriptor.SqlAdapter.AppendQuote("Deleted")}=0 ";
            }

            return sql;
        }
        #endregion
    }
}
