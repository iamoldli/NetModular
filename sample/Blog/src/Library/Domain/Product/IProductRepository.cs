using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Module.Blog.Domain.Product.Models;

namespace Nm.Module.Blog.Domain.Product
{
    /// <summary>
    /// 产品仓储接口
    /// </summary>
    public interface IProductRepository : IRepository<ProductEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IList<ProductEntity>> Query(ProductQueryModel model);

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> UpdateStatus(List<Guid> ids, ProductStatus status);
    }

}
