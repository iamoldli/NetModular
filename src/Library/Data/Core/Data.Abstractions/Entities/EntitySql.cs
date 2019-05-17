using System.Collections.Generic;

namespace Nm.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体的SQL语句
    /// </summary>
    public struct EntitySql
    {
        /// <summary>
        /// 插入语句
        /// </summary>
        public string Insert { get; set; }

        /// <summary>
        /// 批量插入语句
        /// </summary>
        public string BatchInsert { get; set; }

        /// <summary>
        /// 批量插入列集合
        /// </summary>
        public List<IColumnDescriptor> BatchInsertColumnList { get; set; }

        /// <summary>
        /// 删除单条语句
        /// </summary>
        public string DeleteSingle { get; set; }

        /// <summary>
        /// 删除语句
        /// </summary>
        public string Delete { get; set; }

        /// <summary>
        /// 软删除语句
        /// </summary>
        public string SoftDelete { get; set; }

        /// <summary>
        /// 软删除单条数据
        /// </summary>
        public string SoftDeleteSingle { get; set; }

        /// <summary>
        /// 修改单条语句
        /// </summary>
        public string UpdateSingle { get; set; }

        /// <summary>
        /// 修改语句
        /// </summary>
        public string Update { get; set; }

        /// <summary>
        /// 查询单条语句
        /// </summary>
        public string Get { get; set; }

        /// <summary>
        /// 查询语句
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 是否存在语句
        /// </summary>
        public string Exists { get; set; }
    }
}
