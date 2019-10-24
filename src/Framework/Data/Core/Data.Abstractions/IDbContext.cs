using System;
using System.Data;
using Nm.Lib.Auth.Abstractions;
using Nm.Lib.Data.Abstractions.Entities;

namespace Nm.Lib.Data.Abstractions
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// 服务提供器
        /// </summary>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 登录信息
        /// </summary>
        ILoginInfo LoginInfo { get; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        IDbContextOptions Options { get; }

        /// <summary>
        /// 创建新的连接
        /// </summary>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        IDbConnection NewConnection(IDbTransaction transaction = null);

        /// <summary>
        /// 创建新的工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork NewUnitOfWork();

        /// <summary>
        /// 创建新的工作单元
        /// </summary>
        /// <returns></returns>
        IUnitOfWork NewUnitOfWork(IsolationLevel isolationLevel);

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
