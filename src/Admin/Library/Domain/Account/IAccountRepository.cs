using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Auth.Abstractions;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Account.Models;

namespace NetModular.Module.Admin.Domain.Account
{
    /// <summary>
    /// 账户仓储
    /// </summary>
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">账户编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        Task<bool> UpdatePassword(Guid id, string password);

        /// <summary>
        /// 根据用户名查询账户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="type">账户类型</param>
        /// <returns></returns>
        Task<AccountEntity> GetByUserName(string userName, AccountType type);

        /// <summary>
        /// 根据邮箱查询账户实体
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="type">账户类型</param>
        /// <returns></returns>
        Task<AccountEntity> GetByEmail(string email, AccountType type);

        /// <summary>
        /// 根据手机号查询账户实体
        /// </summary>
        /// <param name="phone">邮箱</param>
        /// <param name="type">账户类型</param>
        /// <returns></returns>
        Task<AccountEntity> GetByPhone(string phone, AccountType type);

        /// <summary>
        /// 根据用户名或邮箱获取实体
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<AccountEntity> GetByUserNameOrEmail(string keyword, AccountType type = AccountType.Admin);

        /// <summary>
        /// 修改登录信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status">状态</param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> UpdateAccountStatus(Guid id, AccountStatus status, IUnitOfWork uow = null);

        /// <summary>
        /// 查询账户列表
        /// </summary>
        /// <returns></returns>
        Task<IList<AccountEntity>> Query(AccountQueryModel model);

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="id">编号</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<bool> ExistsUserName(string userName, Guid? id = null, AccountType type = AccountType.Admin);

        /// <summary>
        /// 手机号是否存在
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="id">编号</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<bool> ExistsPhone(string phone, Guid? id = null, AccountType type = AccountType.Admin);

        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="id">编号</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        Task<bool> ExistsEmail(string email, Guid? id = null, AccountType type = AccountType.Admin);
    }
}
