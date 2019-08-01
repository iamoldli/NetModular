using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Button.Models;

namespace Nm.Module.Admin.Domain.Button
{
    /// <summary>
    /// 按钮仓储
    /// </summary>
    public interface IButtonRepository : IRepository<ButtonEntity>
    {
        /// <summary>
        /// 查询按钮列表
        /// </summary>
        /// <returns></returns>
        Task<IList<ButtonEntity>> Query(ButtonQueryModel model);

        /// <summary>
        /// 根据菜单编号查询按钮列表
        /// </summary>
        /// <param name="menuCode"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<IList<ButtonEntity>> QueryByMenu(string menuCode, IDbTransaction transaction);

        /// <summary>
        /// 查询指定账户拥有的按钮编码列表
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> QueryCodeByAccount(Guid accountId);

        /// <summary>
        /// 删除指定菜单的按钮
        /// </summary>
        /// <param name="menuCode">菜单编码</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenu(string menuCode, IDbTransaction transaction);

        /// <summary>
        /// 同步操作更新按钮信息
        /// </summary>
        /// <param name="button"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> UpdateForSync(ButtonEntity button, IDbTransaction transaction);
    }
}