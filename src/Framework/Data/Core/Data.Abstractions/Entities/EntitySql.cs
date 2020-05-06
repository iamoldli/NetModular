using System.Collections.Generic;
using NetModular.Lib.Data.Abstractions.Enums;

namespace NetModular.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体的SQL语句
    /// </summary>
    public class EntitySql
    {
        #region ==字段==

        /// <summary>
        /// 插入语句
        /// </summary>
        private readonly string _insert;

        /// <summary>
        /// 批量插入语句
        /// </summary>
        private readonly string _batchInsert;

        /// <summary>
        /// 删除单条语句
        /// </summary>
        private readonly string _deleteSingle;

        /// <summary>
        /// 删除语句
        /// </summary>
        private readonly string _delete;

        /// <summary>
        /// 软删除语句
        /// </summary>
        private readonly string _softDelete;

        /// <summary>
        /// 软删除单条数据
        /// </summary>
        private readonly string _softDeleteSingle;

        /// <summary>
        /// 修改单条语句
        /// </summary>
        private readonly string _updateSingle;

        /// <summary>
        /// 修改语句
        /// </summary>
        private readonly string _update;

        /// <summary>
        /// 查询单条语句
        /// </summary>
        private readonly string _get;

        /// <summary>
        /// 查询单条语句并行锁
        /// </summary>
        private readonly string _getAndRowLock;

        /// <summary>
        /// 查询单条语句并排除锁(仅SqlServer可用)
        /// </summary>
        private readonly string _getAndNoLock;

        /// <summary>
        /// 查询语句
        /// </summary>
        private readonly string _query;

        /// <summary>
        /// 是否存在语句
        /// </summary>
        private readonly string _exists;

        private readonly IEntityDescriptor _descriptor;

        private readonly ISqlAdapter _adapter;

        #endregion

        public EntitySql(IEntityDescriptor descriptor, string insert, string batchInsert, string deleteSingle, string delete, string softDelete, string softDeleteSingle, string updateSingle, string update, string get, string getAndRowLock, string getAndNoLockSql, string query, string exists, List<IColumnDescriptor> batchInsertColumnList)
        {
            _descriptor = descriptor;
            _adapter = _descriptor.SqlAdapter;
            _insert = insert;
            _batchInsert = batchInsert;
            _deleteSingle = deleteSingle;
            _delete = delete;
            _softDelete = softDelete;
            _softDeleteSingle = softDeleteSingle;
            _updateSingle = updateSingle;
            _update = update;
            _get = get;
            _getAndRowLock = getAndRowLock;
            _getAndNoLock = getAndNoLockSql;
            _query = query;
            _exists = exists;
            BatchInsertColumnList = batchInsertColumnList;
        }

        /// <summary>
        /// 批量插入列集合
        /// </summary>
        public List<IColumnDescriptor> BatchInsertColumnList { get; set; }

        #region ==插入==

        private string _defaultInsert;
        /// <summary>
        /// 获取插入语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Insert(string tableName)
        {
            if (tableName.NotNull())
            {
                return string.Format(_insert, GetTableName(tableName));
            }

            if (_defaultInsert.IsNull())
            {
                _defaultInsert = string.Format(_insert, GetTableName(_descriptor.TableName));
            }

            return _defaultInsert;
        }

        #endregion

        #region ==批量插入==

        private string _defaultBatchInsert;
        /// <summary>
        /// 获取批量插入语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string BatchInsert(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_batchInsert, GetTableName(tableName));

            if (_defaultBatchInsert.IsNull())
            {
                _defaultBatchInsert = string.Format(_batchInsert, GetTableName(_descriptor.TableName));
            }

            return _defaultBatchInsert;
        }

        #endregion

        #region ==删除单条==

        private string _defaultDeleteSingle;
        /// <summary>
        /// 获取单条删除语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string DeleteSingle(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_deleteSingle, GetTableName(tableName));

            if (_defaultDeleteSingle.IsNull())
            {
                _defaultDeleteSingle = string.Format(_deleteSingle, GetTableName(_descriptor.TableName));
            }

            return _defaultDeleteSingle;
        }

        #endregion

        #region ==条件删除==

        private string _defaultDelete;
        /// <summary>
        /// 获取条件删除语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Delete(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_delete, GetTableName(tableName));

            if (_defaultDelete.IsNull())
            {
                _defaultDelete = string.Format(_delete, GetTableName(_descriptor.TableName));
            }

            return _defaultDelete;
        }

        #endregion

        #region ==软删除单条==

        private string _defaultSoftDeleteSingle;
        /// <summary>
        /// 获取软删除单条语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string SoftDeleteSingle(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_softDeleteSingle, GetTableName(tableName));

            if (_defaultSoftDeleteSingle.IsNull())
            {
                _defaultSoftDeleteSingle = string.Format(_softDeleteSingle, GetTableName(_descriptor.TableName));
            }

            return _defaultSoftDeleteSingle;
        }

        #endregion

        #region ==软删除批量==

        private string _defaultSoftDelete;
        /// <summary>
        /// 获取软删除单条语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string SoftDelete(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_softDelete, GetTableName(tableName));

            if (_defaultSoftDelete.IsNull())
            {
                _defaultSoftDelete = string.Format(_softDelete, GetTableName(_descriptor.TableName));
            }

            return _defaultSoftDelete;
        }

        #endregion

        #region ==更新单个实体==

        private string _defaultUpdateSingle;
        /// <summary>
        /// 获取更新实体语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string UpdateSingle(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_updateSingle, GetTableName(tableName));

            if (_defaultUpdateSingle.IsNull())
            {
                _defaultUpdateSingle = string.Format(_updateSingle, GetTableName(_descriptor.TableName));
            }

            return _defaultUpdateSingle;
        }

        #endregion

        #region ==条件更新==

        private string _defaultUpdate;
        /// <summary>
        /// 获取条件更新实体语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Update(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_update, GetTableName(tableName));

            if (_defaultUpdate.IsNull())
            {
                _defaultUpdate = string.Format(_update, GetTableName(_descriptor.TableName));
            }

            return _defaultUpdate;
        }

        #endregion

        #region ==查询单个实体==

        private string _defaultGet;
        /// <summary>
        /// 获取单个实体语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Get(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_get, GetTableName(tableName));

            if (_defaultGet.IsNull())
            {
                _defaultGet = string.Format(_get, GetTableName(_descriptor.TableName));
            }

            return _defaultGet;
        }

        #endregion

        #region ==查询单个实体带锁==

        private string _defaultGetAdnRowLock;

        /// <summary>
        /// 获取单个实体语句(行锁)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetAndRowLock(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_getAndRowLock, GetTableName(tableName));

            if (_defaultGetAdnRowLock.IsNull())
            {
                _defaultGetAdnRowLock = string.Format(_getAndRowLock, GetTableName(_descriptor.TableName));
            }

            return _defaultGetAdnRowLock;
        }

        #endregion

        #region ==查询单个实体无锁==

        private string _defaultGetAndNoLock;

        /// <summary>
        /// 获取单个实体语句(行锁)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetAndNoLock(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_getAndNoLock, GetTableName(tableName));

            if (_defaultGetAndNoLock.IsNull())
            {
                _defaultGetAndNoLock = string.Format(_getAndNoLock, GetTableName(_descriptor.TableName));
            }

            return _defaultGetAndNoLock;
        }

        #endregion

        #region ==查询语句==

        private string _defaultQuery;
        /// <summary>
        /// 查询语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Query(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_query, GetTableName(tableName));

            if (_defaultQuery.IsNull())
            {
                _defaultQuery = string.Format(_query, GetTableName(_descriptor.TableName));
            }

            return _defaultQuery;
        }

        #endregion

        #region ==存在语句==

        private string _defaultExists;
        /// <summary>
        /// 存在语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Exists(string tableName)
        {
            if (tableName.NotNull())
                return string.Format(_exists, GetTableName(tableName));

            if (_defaultExists.IsNull())
            {
                _defaultExists = string.Format(_exists, GetTableName(_descriptor.TableName));
            }

            return _defaultExists;
        }

        #endregion

        #region ==清空语句==

        private string _defaultClear;

        /// <summary>
        /// 清空语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string Clear(string tableName)
        {
            if (tableName.NotNull())
                return $"DELETE FROM {GetTableName(tableName)}";

            if (_defaultClear.IsNull())
            {
                _defaultClear = $"DELETE FROM {GetTableName(_descriptor.TableName)}";
            }

            return _defaultClear;
        }

        #endregion

        private string GetTableName(string tableName)
        {
            //PostgreSQL数据库需要带上模式(schema)
            return _adapter.SqlDialect == SqlDialect.PostgreSQL ? $"{_adapter.Database}{_adapter.AppendQuote(tableName)}" : _adapter.AppendQuote(tableName);
        }
    }
}
