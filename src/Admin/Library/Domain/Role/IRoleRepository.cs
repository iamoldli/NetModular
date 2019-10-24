using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Admin.Domain.Role.Models;

namespace Nm.Module.Admin.Domain.Role
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
