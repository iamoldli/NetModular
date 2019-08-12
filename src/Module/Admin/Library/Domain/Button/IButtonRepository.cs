using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.Admin.Domain.Button.Models;

namespace Tm.Module.Admin.Domain.Button
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
        /// 编码是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code);
    }
}