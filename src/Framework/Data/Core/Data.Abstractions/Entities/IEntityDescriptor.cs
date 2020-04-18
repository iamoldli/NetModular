using System;
using NetModular.Lib.Data.Abstractions.Options;

namespace NetModular.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体信息
    /// </summary>
    public interface IEntityDescriptor
    {
        /// <summary>
        /// 数据库配置信息
        /// </summary>
        DbOptions DbOptions { get; }

        /// <summary>
        /// 模块配置信息
        /// </summary>
        DbModuleOptions ModuleOptions { get; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        string Database { get; }

        /// <summary>
        /// 模块名称
        /// </summary>
        string ModuleName { get; }

        /// <summary>
        /// 表名称
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 不创建表
        /// </summary>
        bool Ignore { get; }

        /// <summary>
        /// 实体类型
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// 列集合
        /// </summary>
        IColumnDescriptorCollection Columns { get; }

        /// <summary>
        /// 主键列
        /// </summary>
        IPrimaryKeyDescriptor PrimaryKey { get; }
        
        /// <summary>
        /// SQL语句
        /// </summary>
        EntitySql Sql { get; }

        /// <summary>
        /// 数据库适配器
        /// </summary>
        ISqlAdapter SqlAdapter { get; }

        /// <summary>
        /// 是否是EntityBase类型实体
        /// </summary>
        bool IsEntityBase { get; }

        /// <summary>
        /// 是否包含软删除
        /// </summary>
        bool IsSoftDelete { get; }

        /// <summary>
        /// 是否多租户
        /// </summary>
        bool IsTenant { get; }

        /// <summary>
        /// 租户编号对应数据库字段名称
        /// </summary>
        string TenantIdColumnName { get; }
    }
}
