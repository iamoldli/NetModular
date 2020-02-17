using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Button.Models;

namespace NetModular.Module.Admin.Domain.Button
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
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<IList<ButtonEntity>> QueryByMenu(string menuCode, IUnitOfWork uow);

        /// <summary>
        /// 查询指定账户拥有的按钮编码列表
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> QueryCodeByAccount(Guid accountId);

        /// <summary>
        /// 编码是否存在
        /// </summary>
        /// <param name="code"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code, IUnitOfWork uow);
    }
}