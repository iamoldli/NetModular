using System;
using System.Collections.Generic;
using System.Linq;

namespace Nm.Lib.Data.Abstractions.Entities
{
    /// <summary>
    /// 实体信息集合
    /// </summary>
    public class EntityDescriptorCollection : List<IEntityDescriptor>
    {
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entityDescriptor"></param>
        public new void Add(IEntityDescriptor entityDescriptor)
        {
            if (this.All(m => m.EntityType != entityDescriptor.EntityType))
                base.Add(entityDescriptor);
        }

        /// <summary>
        /// 获取实体信息
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IEntityDescriptor Get<TEntity>() where TEntity : IEntity, new()
        {
            var entity = this.FirstOrDefault(m => m.EntityType == typeof(TEntity));
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(TEntity), "实体不存在");
            }

            return entity;
        }
    }
}
