using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Logging;
using Tm.Lib.Data.Abstractions;
using Tm.Lib.Data.Abstractions.Entities;
using Tm.Lib.Data.Abstractions.SqlQueryable;
using Tm.Lib.Data.Core.Internal;
using Tm.Lib.Data.Core.SqlQueryable;
using CommonExtensions = Tm.Lib.Data.Core.Internal.CommonExtensions;

namespace Tm.Lib.Data.Core
{
    public class DbSet<TEntity> : IDbSet<TEntity> where TEntity : IEntity, new()
    {
        #region ==属性==

        public IDbContext DbContext { get; }

        public IEntityDescriptor EntityDescriptor { get; }

        #endregion

        #region ==字段==

        private readonly ISqlAdapter _sqlAdapter;

        private readonly EntitySql _sql;

        private readonly ILogger _logger;

        #endregion

        #region ==构造函数==

        public DbSet(IDbContext context)
        {
            DbContext = context;
            EntityDescriptor = EntityDescriptorCollection.Get<TEntity>();
            _sqlAdapter = context.Options.SqlAdapter;
            _sql = EntityDescriptor.Sql;

            _logger = context.Options.LoggerFactory?.CreateLogger("DbSet-" + EntityDescriptor.TableName);
        }

        #endregion

        #region ==Insert==

        public bool Insert(TEntity entity, IDbTransaction transaction = null, string tableName = null)
        {
            Check.NotNull(entity, nameof(entity));

            SetCreatedBy(entity);

            var sql = _sql.Insert(tableName);

            if (EntityDescriptor.PrimaryKey.IsInt())
            {
                sql += _sqlAdapter.IdentitySql;
                var id = ExecuteScalar<int>(sql, entity, transaction);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    return true;
                }

                return false;
            }
            if (EntityDescriptor.PrimaryKey.IsLong())
            {
                sql += _sqlAdapter.IdentitySql;
                var id = ExecuteScalar<long>(sql, entity, transaction);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    return true;
                }
                return false;
            }

            if (EntityDescriptor.PrimaryKey.IsGuid())
            {
                var id = (Guid)EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                if (id == Guid.Empty)
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, _sqlAdapter.GenerateSequentialGuid());

                _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                return Execute(sql, entity, transaction) > 0;
            }

            _logger?.LogDebug("Insert:({0})", sql);
            return Execute(sql, entity, transaction) > 0;

        }

        public async Task<bool> InsertAsync(TEntity entity, IDbTransaction transaction = null, string tableName = null)
        {
            Check.NotNull(entity, nameof(entity));
            SetCreatedBy(entity);

            var sql = _sql.Insert(tableName);

            if (EntityDescriptor.PrimaryKey.IsInt())
            {
                sql += _sqlAdapter.IdentitySql;

                var id = await ExecuteScalarAsync<int>(sql, entity, transaction);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    return true;
                }

                return false;
            }
            if (EntityDescriptor.PrimaryKey.IsLong())
            {
                sql += _sqlAdapter.IdentitySql;
                var id = await ExecuteScalarAsync<long>(sql, entity, transaction);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    return true;
                }
                return false;
            }
            if (EntityDescriptor.PrimaryKey.IsGuid())
            {
                var id = (Guid)EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                if (id == Guid.Empty)
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, _sqlAdapter.GenerateSequentialGuid());

                _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                return await ExecuteAsync(sql, entity, transaction) > 0;
            }

            _logger?.LogDebug("Insert:({0})", sql);

            return await ExecuteAsync(sql, entity, transaction) > 0;

        }

        #endregion

        #region ==BatchInsert==

        public bool BatchInsert(List<TEntity> entityList, int flushSize = 10000, IDbTransaction transaction = null, string tableName = null)
        {
            if (entityList == null || !entityList.Any())
                return false;

            //判断有没有事务
            var hasTran = true;
            if (transaction == null)
            {
                transaction = DbContext.BeginTransaction();
                hasTran = false;
            }

            try
            {
                if (_sqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                {
                    #region ==SQLite使用Dapper的官方方法==

                    if (EntityDescriptor.PrimaryKey.IsGuid())
                    {
                        entityList.ForEach(entity =>
                        {
                            SetCreatedBy(entity);

                            var value = EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                            if ((Guid)value == Guid.Empty)
                            {
                                value = _sqlAdapter.GenerateSequentialGuid();
                                EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, value);
                            }
                        });
                    }

                    Execute(_sql.Insert(tableName), entityList);

                    #endregion
                }
                else
                {
                    #region ==自定义==

                    var sqlBuilder = new StringBuilder();

                    for (var t = 0; t < entityList.Count; t++)
                    {
                        var mod = (t + 1) % flushSize;
                        if (mod == 1)
                        {
                            sqlBuilder.Clear();
                            sqlBuilder.Append(_sql.BatchInsert(tableName));
                        }

                        var entity = entityList[t];
                        SetCreatedBy(entity);

                        sqlBuilder.Append("(");
                        for (var i = 0; i < _sql.BatchInsertColumnList.Count; i++)
                        {
                            var col = _sql.BatchInsertColumnList[i];
                            var value = col.PropertyInfo.GetValue(entity);
                            var type = col.PropertyInfo.PropertyType;

                            if (col.IsPrimaryKey && EntityDescriptor.PrimaryKey.IsGuid())
                            {
                                if ((Guid)value == Guid.Empty)
                                {
                                    value = _sqlAdapter.GenerateSequentialGuid();
                                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, value);
                                }
                            }

                            AppendValue(sqlBuilder, type, value);

                            if (i < _sql.BatchInsertColumnList.Count - 1)
                                sqlBuilder.Append(",");
                        }

                        sqlBuilder.Append(")");

                        if (mod > 0 && t < entityList.Count - 1)
                            sqlBuilder.Append(",");
                        else if (mod == 0 || t == entityList.Count - 1)
                        {
                            sqlBuilder.Append(";");
                            Execute(sqlBuilder.ToString(), transaction: transaction);
                        }
                    }

                    #endregion
                }

                if (!hasTran)
                    transaction.Commit();

                return true;
            }
            catch
            {
                if (!hasTran)
                    transaction.Rollback();

                throw;
            }
        }

        public async Task<bool> BatchInsertAsync(List<TEntity> entityList, int flushSize = 10000, IDbTransaction transaction = null, string tableName = null)
        {
            if (entityList == null || !entityList.Any())
                return false;

            //判断有没有事务
            var hasTran = true;
            if (transaction == null)
            {
                transaction = DbContext.BeginTransaction();
                hasTran = false;
            }

            try
            {
                if (_sqlAdapter.SqlDialect == Abstractions.Enums.SqlDialect.SQLite)
                {
                    #region ==SQLite使用Dapper的官方方法==

                    if (EntityDescriptor.PrimaryKey.IsGuid())
                    {
                        entityList.ForEach(entity =>
                        {
                            SetCreatedBy(entity);

                            var value = EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                            if ((Guid)value == Guid.Empty)
                            {
                                value = _sqlAdapter.GenerateSequentialGuid();
                                EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, value);
                            }
                        });
                    }

                    await ExecuteAsync(_sql.Insert(tableName), entityList);

                    #endregion
                }
                else
                {
                    #region ==自定义方法==

                    var sqlBuilder = new StringBuilder();

                    for (var t = 0; t < entityList.Count; t++)
                    {
                        var mod = (t + 1) % flushSize;
                        if (mod == 1)
                        {
                            sqlBuilder.Clear();
                            sqlBuilder.Append(_sql.BatchInsert(tableName));
                        }

                        var entity = entityList[t];

                        SetCreatedBy(entity);

                        sqlBuilder.Append("(");
                        for (var i = 0; i < _sql.BatchInsertColumnList.Count; i++)
                        {
                            var col = _sql.BatchInsertColumnList[i];
                            var value = col.PropertyInfo.GetValue(entity);
                            var type = col.PropertyInfo.PropertyType;

                            //是否GUID主键
                            if (col.IsPrimaryKey && EntityDescriptor.PrimaryKey.IsGuid())
                            {
                                if ((Guid)value == Guid.Empty)
                                {
                                    value = _sqlAdapter.GenerateSequentialGuid();
                                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, value);
                                }
                            }

                            AppendValue(sqlBuilder, type, value);

                            if (i < _sql.BatchInsertColumnList.Count - 1)
                                sqlBuilder.Append(",");
                        }

                        sqlBuilder.Append(")");

                        if (mod > 0 && t < entityList.Count - 1)
                            sqlBuilder.Append(",");
                        else if (mod == 0 || t == entityList.Count - 1)
                        {
                            sqlBuilder.Append(";");
                            await ExecuteAsync(sqlBuilder.ToString(), transaction: transaction);
                        }
                    }

                    #endregion
                }

                if (!hasTran)
                    transaction.Commit();

                return true;
            }
            catch
            {
                if (!hasTran)
                    transaction.Rollback();

                throw;
            }
        }

        #endregion

        #region ==Delete==

        private DynamicParameters GetDeleteParameters(dynamic id)
        {
            PrimaryKeyValidate(id);

            var dynParams = new DynamicParameters();
            dynParams.Add(_sqlAdapter.AppendParameter("Id"), id);
            return dynParams;
        }

        public bool Delete(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            var dynParams = GetDeleteParameters(id);
            return Execute(_sql.DeleteSingle(tableName), dynParams, transaction) > 0;
        }

        public async Task<bool> DeleteAsync(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            var dynParams = GetDeleteParameters(id);
            return await ExecuteAsync(_sql.DeleteSingle(tableName), dynParams, transaction) > 0;
        }

        #endregion

        #region ==SoftDelete==

        private DynamicParameters GetSoftDeleteParameters(dynamic id)
        {
            PrimaryKeyValidate(id);
            var dynParams = new DynamicParameters();
            dynParams.Add(_sqlAdapter.AppendParameter("Id"), id);
            dynParams.Add(_sqlAdapter.AppendParameter("DeletedTime"), DateTime.Now);

            var deleteBy = Guid.Empty;
            if (DbContext.LoginInfo != null)
            {
                deleteBy = DbContext.LoginInfo.AccountId;
            }
            dynParams.Add(_sqlAdapter.AppendParameter("DeletedBy"), deleteBy);


            return dynParams;
        }

        public bool SoftDelete(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            if (!EntityDescriptor.SoftDelete)
                throw new Exception("该实体未继承软删除实体，无法使用软删除功能~");

            var dynParams = GetSoftDeleteParameters(id);

            return Execute(_sql.SoftDeleteSingle(tableName), dynParams, transaction) > 0;
        }

        public async Task<bool> SoftDeleteAsync(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            if (!EntityDescriptor.SoftDelete)
                throw new Exception("该实体未继承软删除实体，无法使用软删除功能~");

            var dynParams = GetSoftDeleteParameters(id);
            return await ExecuteAsync(_sql.SoftDeleteSingle(tableName), dynParams, transaction) > 0;
        }

        #endregion

        #region ==Update==

        private void UpdateCheck(TEntity entity)
        {
            Check.NotNull(entity, nameof(entity));

            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("没有主键的实体对象无法使用该方法", nameof(entity));

            SetModifiedBy(entity);
        }

        public bool Update(TEntity entity, IDbTransaction transaction = null, string tableName = null)
        {
            UpdateCheck(entity);
            return Execute(_sql.UpdateSingle(tableName), entity, transaction) > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity, IDbTransaction transaction = null, string tableName = null)
        {
            UpdateCheck(entity);
            return await ExecuteAsync(_sql.UpdateSingle(tableName), entity, transaction) > 0;
        }

        #endregion

        #region ==Get==

        private DynamicParameters GetParameters(dynamic id)
        {
            PrimaryKeyValidate(id);

            var dynParams = new DynamicParameters();
            dynParams.Add(_sqlAdapter.AppendParameter("Id"), id);
            return dynParams;
        }
        public TEntity Get(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            var dynParams = GetParameters(id);
            return QuerySingleOrDefault<TEntity>(_sql.Get(tableName), dynParams, transaction);
        }

        public Task<TEntity> GetAsync(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            var dynParams = GetParameters(id);
            return QuerySingleOrDefaultAsync<TEntity>(_sql.Get(tableName), dynParams, transaction);
        }

        #endregion

        #region ==Exists==

        public bool Exists(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            //没有主键的表无法使用Exists方法
            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("该实体没有主键，无法使用Exists方法~");

            var dynParams = GetParameters(id);
            return QuerySingleOrDefault<int>(_sql.Exists(tableName), dynParams, transaction) > 0;
        }

        public async Task<bool> ExistsAsync(dynamic id, IDbTransaction transaction = null, string tableName = null)
        {
            //没有主键的表无法使用Exists方法
            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("该实体没有主键，无法使用Exists方法~");

            var dynParams = GetParameters(id);
            return (await QuerySingleOrDefaultAsync<int>(_sql.Exists(tableName), dynParams, transaction)) > 0;
        }

        #endregion

        #region ==Execute==

        public int Execute(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).Execute(sql, param, transaction, commandType: commandType);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).ExecuteAsync(sql, param, transaction, commandType: commandType);
        }

        #endregion

        #region ==ExecuteScalar==

        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).ExecuteScalar<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).ExecuteScalarAsync<T>(sql, param, transaction, commandType: commandType);
        }

        #endregion

        #region ==QueryFirstOrDefault==

        public dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryFirstOrDefault(sql, param, transaction, commandType: commandType);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryFirstOrDefault<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryFirstOrDefaultAsync(sql, param, transaction, commandType: commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        #endregion

        #region ==QuerySingleOrDefault==

        public dynamic QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QuerySingleOrDefault(sql, param, transaction, commandType: commandType);
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QuerySingleOrDefault<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QuerySingleOrDefaultAsync(sql, param, transaction, commandType: commandType);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandType: commandType);
        }

        #endregion

        #region ==Query==

        public IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).Query(sql, param, transaction, commandType: commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).Query<T>(sql, param, transaction, commandType: commandType);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryAsync(sql, param, transaction, commandType: commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CommandType? commandType = null)
        {
            return DbContext.NewConnection(transaction).QueryAsync<T>(sql, param, transaction, commandType: commandType);
        }

        public INetSqlQueryable<TEntity> Find()
        {
            return new NetSqlQueryable<TEntity>(this, null, null);
        }

        public INetSqlQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return new NetSqlQueryable<TEntity>(this, expression, null);
        }

        public INetSqlQueryable<TEntity> Find(string tableName)
        {
            return new NetSqlQueryable<TEntity>(this, null, tableName);
        }

        public INetSqlQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, string tableName)
        {
            return new NetSqlQueryable<TEntity>(this, expression, tableName);
        }

        #endregion

        #region ==分表==

        public string GetTableNameByDay(DateTime date, string tableName = null)
        {
            return $"{tableName ?? EntityDescriptor.TableName}_{date:yyyyMMdd}";
        }

        public string GetTableNameByWeek(DateTime date, string tableName = null)
        {
            var week = date.DayOfYear / 7;
            if (date.DayOfYear % 7 != 0)
            {
                week++;
            }

            return $"{tableName ?? EntityDescriptor.TableName}_{date.Year}{week}";
        }

        public string GetTableNameByMonth(DateTime date, string tableName = null)
        {
            return $"{tableName ?? EntityDescriptor.TableName}_{date:yyyyMM}";
        }

        public string GetTableNameByQuarter(DateTime date, string tableName = null)
        {
            var quarter = 1;
            if (date.Month > 3 && date.Month < 7)
                quarter = 2;
            else if (date.Month > 6 && date.Month < 10)
                quarter = 3;
            else if (date.Month > 9)
                quarter = 4;

            return $"{tableName ?? EntityDescriptor.TableName}_{date.Year}{quarter}";
        }

        public string GetTableNameByYear(DateTime date, string tableName = null)
        {
            return $"{tableName ?? EntityDescriptor.TableName}_{date.Year}";
        }

        #endregion

        #region ==私有方法==

        /// <summary>
        /// 主键验证
        /// </summary>
        /// <param name="id"></param>
        private void PrimaryKeyValidate(dynamic id)
        {
            //没有主键的表无法删除单条记录
            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("该实体没有主键，无法使用该方法~");

            //验证id有效性
            if (EntityDescriptor.PrimaryKey.IsInt() || EntityDescriptor.PrimaryKey.IsLong())
            {
                if (id < 1)
                    throw new ArgumentException("主键不能小于1~");
            }
            else
            {
                if (id == null)
                    throw new ArgumentException("主键不能为空~");
            }
        }

        /// <summary>
        /// 附加值
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        private void AppendValue(StringBuilder sqlBuilder, Type type, object value)
        {
            if (type.IsEnum || type == typeof(bool))
                sqlBuilder.AppendFormat("{0}", CommonExtensions.ToInt(value));
            else if (type == typeof(string) || type == typeof(char) || type == typeof(Guid))
                sqlBuilder.AppendFormat("'{0}'", value);
            else if (type == typeof(DateTime))
                sqlBuilder.AppendFormat("'{0:yyyy-MM-dd HH:mm:ss}'", CommonExtensions.ToDateTime(value));
            else
                sqlBuilder.AppendFormat("{0}", value);
        }

        /// <summary>
        /// 设置添加人以及修改人
        /// </summary>
        /// <param name="entity"></param>
        private void SetCreatedBy(TEntity entity)
        {
            if (EntityDescriptor.IsEntityBase && DbContext.LoginInfo != null)
            {
                int i = 0;
                foreach (var column in EntityDescriptor.Columns)
                {
                    if (column.Name.Equals("CreatedBy") || column.Name.Equals("ModifiedBy"))
                    {
                        var createdBy = (Guid)column.PropertyInfo.GetValue(entity);
                        if (createdBy == Guid.Empty)
                        {
                            createdBy = DbContext.LoginInfo.AccountId;
                            column.PropertyInfo.SetValue(entity, createdBy);
                            i++;
                        }
                    }

                    if (i > 1)
                        break;
                }
            }
        }

        /// <summary>
        /// 设置修改人
        /// </summary>
        /// <param name="entity"></param>
        private void SetModifiedBy(TEntity entity)
        {
            if (EntityDescriptor.IsEntityBase && DbContext.LoginInfo != null)
            {
                int i = 0;
                foreach (var column in EntityDescriptor.Columns)
                {
                    if (column.Name.Equals("ModifiedBy"))
                    {
                        var modifiedBy = (Guid)column.PropertyInfo.GetValue(entity);
                        var accountId = DbContext.LoginInfo.AccountId;
                        if (modifiedBy == Guid.Empty || modifiedBy != accountId)
                        {
                            column.PropertyInfo.SetValue(entity, accountId);
                            i++;
                        }
                    }

                    if (column.Name.Equals("ModifiedTime"))
                    {
                        column.PropertyInfo.SetValue(entity, DateTime.Now);
                        i++;
                    }

                    if (i > 1)
                        break;
                }
            }
        }

        #endregion
    }
}
