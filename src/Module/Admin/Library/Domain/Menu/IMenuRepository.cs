using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;

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
        /// <param name="paging">分页</param>
        /// <param name="name">名称</param>
        /// <param name="code">编码</param>
        /// <param name="parentId">父节点编号</param>
        /// <returns></returns>
        Task<IList<MenuEntity>> Query(Paging paging, string name = null, string code = null, Guid? parentId = null);

        /// <summary>
        /// 判断该节点是否存在子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsChild(Guid id);

        /// <summary>
        /// 在同级别下名称是重复
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsNameByParentId(string name, Guid id, Guid parentId = default(Guid));

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
