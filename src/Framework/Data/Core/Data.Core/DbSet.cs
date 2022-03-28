using Dapper;
using Microsoft.Extensions.Logging;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;
using NetModular.Lib.Data.Abstractions.Enums;
using NetModular.Lib.Data.Abstractions.SqlQueryable;
using NetModular.Lib.Data.Core.SqlQueryable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetModular.Lib.Data.Core
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

        public DbSet(IDbContext context) : this(context, EntityDescriptorCollection.Get<TEntity>()) { }

        public DbSet(IDbContext context, IEntityDescriptor descriptor)
        {
            DbContext = context;
            EntityDescriptor = descriptor;
            _sqlAdapter = context.Options.SqlAdapter;
            _sql = EntityDescriptor.Sql;
            _logger = context.Options.LoggerFactory?.CreateLogger("DbSet-" + EntityDescriptor.TableName);
        }

        #endregion

        #region ==Insert==

        public bool Insert(TEntity entity, IUnitOfWork uow = null, string tableName = null)
        {
            Check.NotNull(entity, nameof(entity));

            SetCreatedBy(entity);

            SetTenant(entity);

            var sql = _sql.Insert(tableName);

            if (EntityDescriptor.PrimaryKey.IsInt())
            {
                //自增主键
                sql += _sqlAdapter.IdentitySql;
                var id = ExecuteScalar<int>(sql, entity, uow);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                    return true;
                }

                return false;
            }

            if (EntityDescriptor.PrimaryKey.IsLong())
            {
                //自增主键
                sql += _sqlAdapter.IdentitySql;
                var id = ExecuteScalar<long>(sql, entity, uow);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                    return true;
                }
                return false;
            }

            if (EntityDescriptor.PrimaryKey.IsGuid())
            {
                var id = (Guid)EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                if (id == Guid.Empty)
                {
                    id = _sqlAdapter.GenerateSequentialGuid();
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);
                }

                _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                var result = Execute(sql, entity, uow) > 0;
                if (result)
                {
                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                }
                return result;
            }

            _logger?.LogDebug("Insert:({0})", sql);
            return Execute(sql, entity, uow) > 0;

        }

        public async Task<bool> InsertAsync(TEntity entity, IUnitOfWork uow = null, string tableName = null)
        {
            Check.NotNull(entity, nameof(entity));

            SetCreatedBy(entity);

            SetTenant(entity);

            var sql = _sql.Insert(tableName);

            if (EntityDescriptor.PrimaryKey.IsInt())
            {
                sql += _sqlAdapter.IdentitySql;

                var id = await ExecuteScalarAsync<int>(sql, entity, uow);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                    return true;
                }

                return false;
            }
            if (EntityDescriptor.PrimaryKey.IsLong())
            {
                sql += _sqlAdapter.IdentitySql;
                var id = await ExecuteScalarAsync<long>(sql, entity, uow);
                if (id > 0)
                {
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);

                    _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                    return true;
                }
                return false;
            }
            if (EntityDescriptor.PrimaryKey.IsGuid())
            {
                var id = (Guid)EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                if (id == Guid.Empty)
                {
                    id = _sqlAdapter.GenerateSequentialGuid();
                    EntityDescriptor.PrimaryKey.PropertyInfo.SetValue(entity, id);
                }

                _logger?.LogDebug("Insert:({0}),NewID({1})", sql, id);

                var result = await ExecuteAsync(sql, entity, uow) > 0;
                if (result)
                {
                    DbContext.ObserverHandler?.Add<TEntity>(id, uow).GetAwaiter().GetResult();
                }
                return result;
            }

            _logger?.LogDebug("Insert:({0})", sql);

            return await ExecuteAsync(sql, entity, uow) > 0;

        }

        #endregion

        #region ==BatchInsert==

        public bool BatchInsert(List<TEntity> entityList, int flushSize = 0, IUnitOfWork uow = null, string tableName = null)
        {
            if (flushSize <= 0)
            {
                flushSize = DbContext.Options.DbOptions.Dialect == SqlDialect.SqlServer ? 1000 : 10000;
            }
            if (entityList == null || !entityList.Any())
                return false;

            //判断有没有事务
            var hasTran = true;
            if (uow == null)
            {
                uow = DbContext.NewUnitOfWork();
                hasTran = false;
            }

            try
            {
                if (_sqlAdapter.SqlDialect == SqlDialect.SQLite)
                {
                    #region ==SQLite使用Dapper的官方方法==

                    if (EntityDescriptor.PrimaryKey.IsGuid())
                    {
                        entityList.ForEach(entity =>
                        {
                            SetCreatedBy(entity);

                            SetTenant(entity);

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

                        SetTenant(entity);

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
                            Execute(sqlBuilder.ToString(), uow: uow);
                        }
                    }

                    #endregion
                }

                if (!hasTran)
                    uow.Commit();

                return true;
            }
            catch
            {
                if (!hasTran)
                    uow.Rollback();

                throw;
            }
        }

        public async Task<bool> BatchInsertAsync(List<TEntity> entityList, int flushSize = 0, IUnitOfWork uow = null, string tableName = null)
        {
            if (flushSize <= 0)
            {
                flushSize = DbContext.Options.DbOptions.Dialect == SqlDialect.SqlServer ? 1000 : 10000;
            }
            if (entityList == null || !entityList.Any())
                return false;

            //判断有没有事务
            var hasTran = true;
            if (uow == null)
            {
                uow = DbContext.NewUnitOfWork();
                hasTran = false;
            }

            try
            {
                if (_sqlAdapter.SqlDialect == SqlDialect.SQLite)
                {
                    #region ==SQLite使用Dapper的官方方法==

                    if (EntityDescriptor.PrimaryKey.IsGuid())
                    {
                        entityList.ForEach(entity =>
                        {
                            SetCreatedBy(entity);
                            SetTenant(entity);

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
                        SetTenant(entity);

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
                            await ExecuteAsync(sqlBuilder.ToString(), uow: uow);
                        }
                    }

                    #endregion
                }

                if (!hasTran)
                    uow.Commit();

                return true;
            }
            catch
            {
                if (!hasTran)
                    uow.Rollback();

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

        public bool Delete(dynamic id, IUnitOfWork uow = null, string tableName = null)
        {
            var dynParams = GetDeleteParameters(id);
            var sql = _sql.DeleteSingle(tableName);
            _logger?.LogDebug("Delete:{@sql}", sql);
            var result = Execute(sql, dynParams, uow) > 0;
            if (result)
            {
                DbContext.ObserverHandler?.Delete<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
        }

        public async Task<bool> DeleteAsync(dynamic id, IUnitOfWork uow = null, string tableName = null)
        {
            var dynParams = GetDeleteParameters(id);
            var sql = _sql.DeleteSingle(tableName);
            _logger?.LogDebug("DeleteAsync:{@sql}", sql);
            var result = await ExecuteAsync(sql, dynParams, uow) > 0;
            if (result)
            {
                DbContext.ObserverHandler?.Delete<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
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

        public bool SoftDelete(dynamic id, IUnitOfWork uow = null, string tableName = null)
        {
            if (!EntityDescriptor.SoftDelete)
                throw new Exception("该实体未继承软删除实体，无法使用软删除功能~");

            var dynParams = GetSoftDeleteParameters(id);
            var sql = _sql.SoftDeleteSingle(tableName);
            _logger?.LogDebug("SoftDelete:{@sql}", sql);
            var result = Execute(sql, dynParams, uow) > 0;
            if (result)
            {
                DbContext.ObserverHandler?.Delete<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
        }

        public async Task<bool> SoftDeleteAsync(dynamic id, IUnitOfWork uow = null, string tableName = null)
        {
            if (!EntityDescriptor.SoftDelete)
                throw new Exception("该实体未继承软删除实体，无法使用软删除功能~");

            var dynParams = GetSoftDeleteParameters(id);
            var sql = _sql.SoftDeleteSingle(tableName);
            _logger?.LogDebug("SoftDeleteAsync:{@sql}", sql);
            var result = await ExecuteAsync(sql, dynParams, uow) > 0;
            if (result)
            {
                DbContext.ObserverHandler?.Delete<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
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

        public bool Update(TEntity entity, IUnitOfWork uow = null, string tableName = null)
        {
            UpdateCheck(entity);
            var sql = _sql.UpdateSingle(tableName);
            _logger?.LogDebug("Update:{@sql}", sql);
            var result = Execute(sql, entity, uow) > 0;
            if (result)
            {
                var id = EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                DbContext.ObserverHandler?.Update<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
        }

        public async Task<bool> UpdateAsync(TEntity entity, IUnitOfWork uow = null, string tableName = null)
        {
            UpdateCheck(entity);
            var sql = _sql.UpdateSingle(tableName);
            _logger?.LogDebug("UpdateAsync:{@sql}", sql);
            var result = await ExecuteAsync(sql, entity, uow) > 0;
            if (result)
            {
                var id = EntityDescriptor.PrimaryKey.PropertyInfo.GetValue(entity);
                DbContext.ObserverHandler?.Update<TEntity>(id, uow).GetAwaiter().GetResult();
            }
            return result;
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

        public TEntity Get(dynamic id, IUnitOfWork uow = null, string tableName = null, bool rowLock = false, bool noLock = false)
        {
            var dynParams = GetParameters(id);
            string sql;
            if (rowLock)
                sql = _sql.GetAndRowLock(tableName);
            else if (_sqlAdapter.SqlDialect == SqlDialect.SqlServer && noLock)
                sql = _sql.GetAndNoLock(tableName);
            else
                sql = _sql.Get(tableName);

            _logger?.LogDebug("Get:{@sql}", sql);
            return QuerySingleOrDefault<TEntity>(sql, dynParams, uow);
        }

        public Task<TEntity> GetAsync(dynamic id, IUnitOfWork uow = null, string tableName = null, bool rowLock = false, bool noLock = false)
        {
            var dynParams = GetParameters(id);
            string sql;
            if (rowLock)
                sql = _sql.GetAndRowLock(tableName);
            else if (_sqlAdapter.SqlDialect == SqlDialect.SqlServer && noLock)
                sql = _sql.GetAndNoLock(tableName);
            else
                sql = _sql.Get(tableName);

            _logger?.LogDebug("GetAsync:{@sql}", sql);
            return QuerySingleOrDefaultAsync<TEntity>(sql, dynParams, uow);
        }

        #endregion

        #region ==Exists==

        public bool Exists(dynamic id, IUnitOfWork uow = null, string tableName = null, bool noLock = false)
        {
            //没有主键的表无法使用Exists方法
            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("该实体没有主键，无法使用Exists方法~");

            var dynParams = GetParameters(id);
            var sql = _sql.Exists(tableName);

            _logger?.LogDebug("Exists:{@sql}", sql);

            return QuerySingleOrDefault<int>(sql, dynParams, uow) > 0;
        }

        public async Task<bool> ExistsAsync(dynamic id, IUnitOfWork uow = null, string tableName = null, bool noLock = false)
        {
            //没有主键的表无法使用Exists方法
            if (EntityDescriptor.PrimaryKey.IsNo())
                throw new ArgumentException("该实体没有主键，无法使用Exists方法~");

            var dynParams = GetParameters(id);
            var sql = _sql.Exists(tableName);

            _logger?.LogDebug("ExistsAsync:{@sql}", sql);
            return (await QuerySingleOrDefaultAsync<int>(sql, dynParams, uow)) > 0;
        }

        #endregion

        #region ==Execute==

        public int Execute(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).Execute(sql, param, tran, commandType: commandType);
        }

        public Task<int> ExecuteAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).ExecuteAsync(sql, param, tran, commandType: commandType);
        }

        #endregion

        #region ==ExecuteScalar==

        public T ExecuteScalar<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).ExecuteScalar<T>(sql, param, tran, commandType: commandType);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).ExecuteScalarAsync<T>(sql, param, tran, commandType: commandType);
        }

        #endregion

        #region ==ExecuteReader==

        public IDataReader ExecuteReader(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).ExecuteReader(sql, param, tran, commandType: commandType);
        }

        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).ExecuteReaderAsync(sql, param, tran, commandType: commandType);
        }

        #endregion

        #region ==QueryFirstOrDefault==

        public dynamic QueryFirstOrDefault(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryFirstOrDefault(sql, param, tran, commandType: commandType);
        }

        public T QueryFirstOrDefault<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryFirstOrDefault<T>(sql, param, tran, commandType: commandType);
        }

        public Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryFirstOrDefaultAsync(sql, param, tran, commandType: commandType);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryFirstOrDefaultAsync<T>(sql, param, tran, commandType: commandType);
        }

        #endregion

        #region ==QuerySingleOrDefault==

        public dynamic QuerySingleOrDefault(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QuerySingleOrDefault(sql, param, tran, commandType: commandType);
        }

        public T QuerySingleOrDefault<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QuerySingleOrDefault<T>(sql, param, tran, commandType: commandType);
        }

        public Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QuerySingleOrDefaultAsync(sql, param, tran, commandType: commandType);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QuerySingleOrDefaultAsync<T>(sql, param, tran, commandType: commandType);
        }


        #endregion

        #region ==QueryMultipleAsync==

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryMultiple(sql, param, tran, commandType: commandType);
        }

        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryMultipleAsync(sql, param, tran, commandType: commandType);
        }

        #endregion

        #region ==Query==

        public IEnumerable<dynamic> Query(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).Query(sql, param, tran, commandType: commandType);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).Query<T>(sql, param, tran, commandType: commandType);
        }

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryAsync(sql, param, tran, commandType: commandType);
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IUnitOfWork uow = null, CommandType? commandType = null)
        {
            var tran = GetTransaction(uow);
            return DbContext.NewConnection(tran).QueryAsync<T>(sql, param, tran, commandType: commandType);
        }

        public INetSqlQueryable<TEntity> Find(bool noLock = true)
        {
            return new NetSqlQueryable<TEntity>(this, null, null, noLock);
        }

        public INetSqlQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, bool noLock = true)
        {
            return new NetSqlQueryable<TEntity>(this, expression, null, noLock);
        }

        public INetSqlQueryable<TEntity> Find(string tableName, bool noLock = true)
        {
            return new NetSqlQueryable<TEntity>(this, null, tableName, noLock);
        }

        public INetSqlQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, string tableName, bool noLock = true)
        {
            return new NetSqlQueryable<TEntity>(this, expression, tableName, noLock);
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

        #region ==Clear==

        public bool Clear(IUnitOfWork uow = null, string tableName = null)
        {
            var sql = _sql.Clear(tableName);
            _logger?.LogDebug("Clear:{@sql}", sql);
            Execute(sql, uow: uow);
            return true;
        }

        public async Task<bool> ClearAsync(IUnitOfWork uow = null, string tableName = null)
        {
            var sql = _sql.Clear(tableName);
            _logger?.LogDebug("ClearAsync:{@sql}", sql);
            await ExecuteAsync(sql, uow: uow);
            return true;
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
            if (type.IsNullable())
            {
                type = Nullable.GetUnderlyingType(type);
            }

            if (value == null)
            {
                sqlBuilder.AppendFormat("NULL");
            }
            else if (type.IsEnum)
            {
                sqlBuilder.AppendFormat("{0}", value.ToInt());
            }
            else if (type.IsBool())
            {
                sqlBuilder.AppendFormat("{0}", EntityDescriptor.SqlAdapter.SqlDialect == SqlDialect.PostgreSQL ? value : value.ToInt());
            }
            else if (type.IsDateTime())
            {
                sqlBuilder.AppendFormat("'{0:yyyy-MM-dd HH:mm:ss}'", value);
            }
            else
            {
                sqlBuilder.AppendFormat("'{0}'", value);
            }
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
                    var name = column.PropertyInfo.Name;
                    if (name.Equals("CreatedBy") || name.Equals("ModifiedBy"))
                    {
                        var createdBy = (Guid)column.PropertyInfo.GetValue(entity);
                        if (createdBy == Guid.Empty)
                        {
                            column.PropertyInfo.SetValue(entity, DbContext.LoginInfo.AccountId);
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
                    var name = column.PropertyInfo.Name;
                    if (name.Equals("ModifiedBy"))
                    {
                        var modifiedBy = (Guid)column.PropertyInfo.GetValue(entity);
                        var accountId = DbContext.LoginInfo.AccountId;
                        if (modifiedBy == Guid.Empty)
                        {
                            column.PropertyInfo.SetValue(entity, accountId);
                            i++;
                        }
                    }

                    if (name.Equals("ModifiedTime"))
                    {
                        column.PropertyInfo.SetValue(entity, DateTime.Now);
                        i++;
                    }

                    if (i > 1)
                        break;
                }
            }
        }

        /// <summary>
        /// 设置租户编号
        /// </summary>
        /// <param name="entity"></param>
        private void SetTenant(TEntity entity)
        {
            if (EntityDescriptor.IsTenant && DbContext.LoginInfo != null)
            {
                foreach (var column in EntityDescriptor.Columns)
                {
                    var name = column.PropertyInfo.Name;
                    if (name.Equals("TenantId"))
                    {
                        var tenantId = (Guid?)column.PropertyInfo.GetValue(entity);
                        if (tenantId == null)
                        {
                            column.PropertyInfo.SetValue(entity, DbContext.LoginInfo.TenantId);
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 获取事务
        /// </summary>
        /// <param name="uow"></param>
        /// <returns></returns>
        private IDbTransaction GetTransaction(IUnitOfWork uow)
        {
            return uow?.Transaction;
        }

        #endregion
    }
}
