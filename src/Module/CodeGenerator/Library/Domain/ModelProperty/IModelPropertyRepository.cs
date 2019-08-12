using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Tm.Lib.Data.Abstractions;
using Tm.Module.CodeGenerator.Domain.ModelProperty.Models;

namespace Tm.Module.CodeGenerator.Domain.ModelProperty
{
    public interface IModelPropertyRepository : IRepository<ModelPropertyEntity>
    {
        /// <summary>
        /// 查询指定类的模型属性列表
        /// </summary>
        /// <returns></returns>
        Task<IList<ModelPropertyEntity>> Query(ModelPropertyQueryModel model);

        /// <summary>
        /// 查询所有属性列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ModelPropertyEntity>> QueryAll(ModelPropertyQueryModel model);

        /// <summary>
        /// 查询指定实体的模型属性列表
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<IList<ModelPropertyEntity>> QueryByClass(Guid classId);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(ModelPropertyEntity entity);

        /// <summary>
        /// 删除指定类的所有属性
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByProject(Guid projectId, IDbTransaction transaction);
    }
}
