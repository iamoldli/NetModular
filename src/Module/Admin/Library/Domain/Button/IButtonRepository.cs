using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;

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
        /// <param name="paging">分页</param>
        /// <param name="menuId">管理菜单id</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        Task<IList<ButtonEntity>> Query(Paging paging, Guid menuId, string name = null);

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
        /// <returns></returns>
        Task<bool> DeleteByMenu(Guid menuId);

        /// <summary>
        /// 同步操作更新按钮信息
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        Task<bool> UpdateForSync(ButtonEntity button);
    }
}
