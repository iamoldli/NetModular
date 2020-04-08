using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Core.Extensions;

namespace NetModular.Lib.Data.Core.Entities
{
    internal class EntitySqlBuilder : IEntitySqlBuilder
    {
        /// <summary>
        /// 租户编号占位符
        /// </summary>
        public const string TENANT_ID_PLACEHOLDER = "NM_TENANTID";

        private readonly IEntityDescriptor _descriptor;
        private readonly IPrimaryKeyDescriptor _primaryKey;

        public EntitySqlBuilder(IEntityDescriptor descriptor)
        {
            _descriptor = descriptor;
            _primaryKey = descriptor.PrimaryKey;
        }

        public EntitySql Build()
        {
            var batchInsertColumnList = new List<IColumnDescriptor>();
            var insertSql = BuildInsertSql(batchInsertColumnList, out string batchInsertSql);
            var deleteSql = BuildDeleteSql(out string deleteSingleSql);
            var softDeleteSql = BuildSoftDeleteSql(out string softDeleteSingleSql);
            var updateSql = BuildUpdateSql(out string updateSingleSql);
            var querySql = BuildQuerySql(out string getSql, out string getAdnRowLockSql);
            var existsSql = BuildExistsSql();

            return new EntitySql(_descriptor, insertSql, batchInsertSql, deleteSingleSql, deleteSql, softDeleteSql,
                softDeleteSingleSql, updateSingleSql, updateSql, getSql, getAdnRowLockSql, querySql, existsSql, batchInsertColumnList);
        }

        #region ==Private Methods==

        /// <summary>
        /// 设置插入语句
        /// </summary>
        private string BuildInsertSql(List<IColumnDescriptor> batchInsertColumnList, out string batchInsertSql)
        {
            var sb = new StringBuilder();
            sb.Append("INSERT INTO {0} ");
            sb.Append("(");

            var valuesSql = new StringBuilder();

            //多租户
            if (_descriptor.IsTenant)
            {
                _descriptor.SqlAdapter.AppendQuote(sb, _descriptor.TenantIdColumnName);
                sb.Append(",");

                valuesSql.AppendFormat("{0},", TENANT_ID_PLACEHOLDER);
            }

            foreach (var col in _descriptor.Columns)
            {
                //排除自增主键
                if (col.IsPrimaryKey && (_primaryKey.IsInt() || _primaryKey.IsLong()))
                    continue;

                _descriptor.SqlAdapter.AppendQuote(sb, col.Name);
                sb.Append(",");

                _descriptor.SqlAdapter.AppendParameter(valuesSql, col.PropertyInfo.Name);
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
            sb.Append(")");

            if (_descriptor.SqlAdapter.SqlDialect != SqlDialect.PostgreSQL)
            {
                sb.Append(";");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 设置删除语句
        /// </summary>
        private string BuildDeleteSql(out string deleteSingleSql)
        {
            var deleteSql = "DELETE FROM {0} ";
            if (!_primaryKey.IsNo())
            {
                deleteSingleSql = $"{deleteSql} WHERE {AppendQuote(_primaryKey.Name)}={AppendParameter(_primaryKey.PropertyInfo.Name)}";
                //多租户
                if (_descriptor.IsTenant)
                {
                    deleteSingleSql += $" AND {AppendQuote(_descriptor.TenantIdColumnName)}={TENANT_ID_PLACEHOLDER} ";
                }
            }
            else
                deleteSingleSql = "";

            return deleteSql;
        }

        /// <summary>
        /// 设置软删除
        /// </summary>
        private string BuildSoftDeleteSql(out string softDeleteSingleSql)
        {
            softDeleteSingleSql = string.Empty;
            if (!_descriptor.IsSoftDelete)
            {
                return string.Empty;
            }

            var sb = new StringBuilder("UPDATE {0} SET ");
            sb.AppendFormat("{0}={1},", AppendQuote(_descriptor.GetDeletedColumnName()), _descriptor.SqlAdapter.SqlDialect == SqlDialect.PostgreSQL ? "TRUE" : "1");
            sb.AppendFormat("{0}={1},", AppendQuote(_descriptor.GetDeletedTimeColumnName()), AppendParameter("DeletedTime"));
            sb.AppendFormat("{0}={1} ", AppendQuote(_descriptor.GetDeletedByColumnName()), AppendParameter("DeletedBy"));

            var softDeleteSql = sb.ToString();

            if (!_descriptor.PrimaryKey.IsNo())
            {
                sb.AppendFormat(" WHERE {0}={1}", AppendQuote(_primaryKey.Name), AppendParameter(_primaryKey.PropertyInfo.Name));
                //多租户
                if (_descriptor.IsTenant)
                {
                    sb.AppendFormat(" AND {0}={1};", AppendQuote(_descriptor.TenantIdColumnName), TENANT_ID_PLACEHOLDER);
                }

                softDeleteSingleSql = sb.ToString();
            }

            return softDeleteSql;
        }

        /// <summary>
        /// 设置更新语句
        /// </summary>
        private string BuildUpdateSql(out string updateSingleSql)
        {
            updateSingleSql = string.Empty;
            var sb = new StringBuilder();
            sb.Append("UPDATE {0} SET");

            var updateSql = sb.ToString();
            if (!_primaryKey.IsNo())
            {
                var columns = _descriptor.Columns.Where(m => !m.IsPrimaryKey);

                foreach (var col in columns)
                {
                    sb.AppendFormat("{0}={1}", AppendQuote(col.Name), AppendParameter(col.PropertyInfo.Name));
                    sb.Append(",");
                }

                sb.Remove(sb.Length - 1, 1);

                sb.AppendFormat(" WHERE {0}={1}", AppendQuote(_primaryKey.Name), AppendParameter(_primaryKey.PropertyInfo.Name));
                //多租户
                if (_descriptor.IsTenant)
                {
                    sb.AppendFormat(" AND {0}={1};", AppendQuote(_descriptor.TenantIdColumnName), TENANT_ID_PLACEHOLDER);
                }

                updateSingleSql = sb.ToString();
            }

            return updateSql;
        }

        /// <summary>
        /// 设置查询语句
        /// </summary>
        private string BuildQuerySql(out string getSql, out string getAndRowLockSql)
        {
            var sb = new StringBuilder("SELECT ");
            for (var i = 0; i < _descriptor.Columns.Count; i++)
            {
                var col = _descriptor.Columns[i];
                sb.AppendFormat("{0} AS {1}", AppendQuote(col.Name), AppendQuote(col.PropertyInfo.Name));

                if (i != _descriptor.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.Append(" FROM {0} ");

            var querySql = sb.ToString();
            getSql = querySql;
            getAndRowLockSql = querySql;
            // SqlServer行锁
            if (_descriptor.SqlAdapter.SqlDialect == SqlDialect.SqlServer)
            {
                getAndRowLockSql += " WITH (ROWLOCK, UPDLOCK) ";
            }

            if (!_primaryKey.IsNo())
            {
                getSql += $" WHERE {AppendQuote(_primaryKey.Name)}={AppendParameter(_primaryKey.PropertyInfo.Name)} ";
                getAndRowLockSql += $" WHERE {AppendQuote(_primaryKey.Name)}={AppendParameter(_primaryKey.PropertyInfo.Name)} ";

                //多租户
                if (_descriptor.IsTenant)
                {
                    getSql += $" AND {AppendQuote(_descriptor.TenantIdColumnName)}={TENANT_ID_PLACEHOLDER} ";
                    getAndRowLockSql += $" AND {AppendQuote(_descriptor.TenantIdColumnName)}={TENANT_ID_PLACEHOLDER} ";
                }

                //软删除
                if (_descriptor.IsSoftDelete)
                {
                    var val = _descriptor.SqlAdapter.SqlDialect == SqlDialect.PostgreSQL ? "FALSE" : "0";

                    getSql += $" AND {AppendQuote(_descriptor.GetDeletedColumnName())}={val} ";
                    getAndRowLockSql += $" AND {AppendQuote(_descriptor.GetDeletedColumnName())}={val} ";
                }

                //MySql和PostgreSQL行锁
                if (_descriptor.SqlAdapter.SqlDialect == SqlDialect.MySql || _descriptor.SqlAdapter.SqlDialect == SqlDialect.PostgreSQL)
                {
                    getAndRowLockSql += " FOR UPDATE;";
                }
            }

            return querySql;
        }

        /// <summary>
        /// 设置是否存在语句
        /// </summary>
        /// <returns></returns>
        private string BuildExistsSql()
        {
            //没有主键，无法使用该方法
            if (_primaryKey.IsNo())
                return string.Empty;

            var sql = $"SELECT COUNT(0) FROM {{0}} WHERE {AppendQuote(_primaryKey.Name)}={AppendParameter(_primaryKey.PropertyInfo.Name)}";
            //多租户
            if (_descriptor.IsTenant)
            {
                sql += $" AND {AppendQuote(_descriptor.TenantIdColumnName)}={TENANT_ID_PLACEHOLDER} ";
            }
            //软删除
            if (_descriptor.IsSoftDelete)
            {
                sql += $" AND {AppendQuote(_descriptor.GetDeletedColumnName())}={(_descriptor.SqlAdapter.SqlDialect == SqlDialect.PostgreSQL ? "FALSE" : "0")} ";
            }

            return sql;
        }

        #endregion

        private string AppendQuote(string name)
        {
            return _descriptor.SqlAdapter.AppendQuote(name);
        }

        private string AppendParameter(string name)
        {
            return _descriptor.SqlAdapter.AppendParameter(name);
        }
    }
}
