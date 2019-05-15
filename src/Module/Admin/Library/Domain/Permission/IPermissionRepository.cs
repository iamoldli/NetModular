using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;

namespace NetModular.Module.Admin.Domain.Permission
{
    /// <summary>
    /// 权限仓储
    /// </summary>
    public interface IPermissionRepository : IRepository<PermissionEntity>
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(PermissionEntity entity);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(Guid id);

        /// <summary>
        /// 根据模块编码判断是否存在
        /// </summary>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        Task<bool> ExistsWidthModule(string moduleCode);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="moduleCode"></param>
        /// <param name="name"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> Query(Paging paging, string moduleCode, string name = null, string controller = null, string action = null);

        /// <summary>
        /// 通过菜单编号查找对应权限列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByMenu(Guid menuId);

        /// <summary>
        /// 通过按钮编号查找对应权限列表
        /// </summary>
        /// <param name="buttonId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByButton(Guid buttonId);

        /// <summary>
        /// 查询指定账户的权限列表
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        Task<IList<PermissionEntity>> QueryByAccount(Guid accountId);

        /// <summary>
        /// 修改同步信息
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        Task<bool> UpdateForSync(PermissionEntity permission);
    }
}
