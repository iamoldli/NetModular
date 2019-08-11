using System;
using System.Data;
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
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<bool> DeleteByClass(Guid classId, IDbTransaction transaction);

        /// <summary>
        /// 根据实体查询
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<ClassMethodEntity> GetByClass(Guid classId, IDbTransaction transaction = null);
    }
}
