using System.Collections.Generic;
using System.Threading.Tasks;
using NetModular.Lib.Data.Abstractions;
using NetModular.Module.CodeGenerator.Domain.Enum.Models;

namespace NetModular.Module.CodeGenerator.Domain.Enum
{
    /// <summary>
    /// 枚举仓储
    /// </summary>
    public interface IEnumRepository : IRepository<EnumEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<EnumEntity>> Query(EnumQueryModel model);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<bool> Exists(EnumEntity entity);
    }
}
