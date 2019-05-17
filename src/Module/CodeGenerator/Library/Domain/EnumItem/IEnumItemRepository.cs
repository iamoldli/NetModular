using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.CodeGenerator.Domain.EnumItem.Models;

namespace Nm.Module.CodeGenerator.Domain.EnumItem
{
    /// <summary>
    /// 枚举项仓储
    /// </summary>
    public interface IEnumItemRepository : IRepository<EnumItemEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<EnumItemEntity>> Query(EnumItemQueryModel model);

        /// <summary>
        /// 查询指定枚举的所有项列表
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        Task<IList<EnumItemEntity>> QueryByEnum(Guid enumId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(EnumItemEntity entity);
    }
}
