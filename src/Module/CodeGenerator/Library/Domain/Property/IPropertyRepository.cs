using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.CodeGenerator.Domain.Property.Models;

namespace Nm.Module.CodeGenerator.Domain.Property
{
    /// <summary>
    /// 实体属性信息仓储
    /// </summary>
    public interface IPropertyRepository : IRepository<PropertyEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<PropertyEntity>> Query(PropertyQueryModel model);

        /// <summary>
        /// 查询指定类的所有属性列表
        /// </summary>
        /// <param name="classId">类编号</param>
        /// <returns></returns>
        Task<IList<PropertyEntity>> QueryByClass(Guid classId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(PropertyEntity entity);

        /// <summary>
        /// 是否存在关联指定枚举的数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        Task<bool> ExistsByEnum(Guid enumId);

        /// <summary>
        /// 删除指定类的所有属性
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByClass(Guid classId, IDbTransaction transaction);

        /// <summary>
        /// 删除指定类的所有属性
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction);
    }
}
