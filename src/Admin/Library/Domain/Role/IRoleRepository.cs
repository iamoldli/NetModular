using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.Admin.Domain.Role.Models;

namespace NetModular.Module.Admin.Domain.Role
{
    /// <summary>
    /// 角色仓储
    /// </summary>
    public interface IRoleRepository : IRepository<RoleEntity>
    {
        /// <summary>
        /// 判断角色是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(string name, Guid? id = null);

        Task<IList<RoleEntity>> Query(RoleQueryModel model);
    }
}
