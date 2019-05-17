using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Account.Models;

namespace Nm.Module.Admin.Domain.Account
{
    /// <summary>
    /// 账户仓储
    /// </summary>
    public interface IAccountRepository : IRepository<AccountEntity>
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> UpdatePassword(Guid id, string password);

        /// <summary>
        /// 根据用户名查询账户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<AccountEntity> GetByUserName(string userName);

        /// <summary>
        /// 修改登录信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ip"></param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        Task<bool> UpdateLoginInfo(Guid id, string ip, AccountStatus status = AccountStatus.UnKnown);

        /// <summary>
        /// 查询账户列表
        /// </summary>
        /// <returns></returns>
        Task<IList<AccountEntity>> Query(AccountQueryModel model);

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsUserName(string userName, Guid? id = null);

        /// <summary>
        /// 手机号是否存在
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsPhone(string phone, Guid? id = null);

        /// <summary>
        /// 邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsEmail(string email, Guid? id = null);
    }
}
