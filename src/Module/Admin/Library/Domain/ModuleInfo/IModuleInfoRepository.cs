using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Pagination;

namespace NetModular.Module.Admin.Domain.ModuleInfo
{
    /// <summary>
    /// 模块仓储
    /// </summary>
    public interface IModuleInfoRepository : IRepository<ModuleInfo>
    {
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="paging">分页信息</param>
        /// <param name="name">模块名称</param>
        /// <param name="code">模块编码</param>
        /// <returns></returns>
        Task<IList<ModuleInfo>> Query(Paging paging, string name = null, string code = null);

        /// <summary>
        /// 判断模块是否已存在
        /// </summary>
        /// <param name="code">权限编码</param>
        /// <param name="id">模块编号</param>
        /// <returns></returns>
        Task<bool> Exists(string code, Guid? id = null);
    }
}
