using System;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;

namespace Nm.Module.CodeGenerator.Domain.ClassMethod
{
    public interface IClassMethodRepository : IRepository<ClassMethodEntity>
    {
        /// <summary>
        /// 根据实体删除方法
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<bool> DeleteByClass(Guid classId);

        /// <summary>
        /// 根据实体查询
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        Task<ClassMethodEntity> GetByClass(Guid classId);
    }
}
