using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Menu.Models;

namespace NetModular.Module.Admin.Domain.Menu
{
    /// <summary>
    /// 菜单仓储
    /// </summary>
    public interface IMenuRepository : IRepository<MenuEntity>
    {
        /// <summary>
        /// 查询菜单列表
        /// </summary>
        /// <returns></returns>
        Task<IList<MenuEntity>> Query(MenuQueryModel model);

        /// <summary>
        /// 查询指定菜单的子菜单
        /// </summary>
        /// <returns></returns>
        Task<IList<MenuEntity>> QueryChildren(Guid parentId);

        /// <summary>
        /// 判断该节点是否存在子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsChild(Guid id);

        /// <summary>
        /// 根据模块编码判断是否有菜单
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthModule(string moduleCode);

        /// <summary>
        /// 查询单个菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MenuEntity> GetAsync(Guid id);

        /// <summary>
        /// 获取指定账户的菜单列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<MenuEntity>> GetByAccount(Guid accountId);
    }
}
