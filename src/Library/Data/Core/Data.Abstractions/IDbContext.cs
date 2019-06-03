using System;
using System.Data;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public interface IDbContext : IDisposable
    {
        /// <summary>
        /// 当前登录账户编号
        /// </summary>
        string AccountId { get; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        IDbContextOptions Options { get; }

        /// <summary>
        /// 连接
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 事务
        /// </summary>
        IDbTransaction Transaction { get; set; }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">隔离级别</param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        IDbConnection Open();

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDbSet<TEntity> Set<TEntity>() where TEntity : IEntity, new();
    }

    /// <summary>
    /// 泛型
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    public interface IDbContext<TDbContext> : IDbContext where TDbContext : IDbContext
    {

    }
}
