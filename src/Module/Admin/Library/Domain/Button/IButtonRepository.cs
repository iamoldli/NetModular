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
        /// 判断是否存在
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(string code, Guid? id = null);

        /// <summary>
        /// 根据菜单编号查询按钮列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IList<ButtonEntity>> QueryByMenu(Guid menuId);

        /// <summary>
        /// 查询指定账户拥有的按钮编码列表
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> QueryCodeByAccount(Guid accountId);

        /// <summary>
        /// 删除指定菜单的按钮
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByMenu(Guid menuId, IDbTransaction transaction);

        /// <summary>
        /// 同步操作更新按钮信息
        /// </summary>
        /// <param name="button"></param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        Task<bool> UpdateForSync(ButtonEntity button, IDbTransaction transaction);
    }
}
