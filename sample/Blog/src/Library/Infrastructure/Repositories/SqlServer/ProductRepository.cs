using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nm.Lib.Data.Abstractions;
using Nm.Lib.Data.Core;
using Nm.Lib.Data.Query;
using Nm.Lib.Utils.Core.Extensions;
using Nm.Module.Admin.Domain.Account;
using Nm.Module.Blog.Domain.Product;
using Nm.Module.Blog.Domain.Product.Models;

namespace Nm.Module.Blog.Infrastructure.Repositories.SqlServer
{
    public class ProductRepository : RepositoryAbstract<ProductEntity>, IProductRepository
    {
        public ProductRepository(IDbContext context) : base(context)
        {
        }

        public async Task<IList<ProductEntity>> Query(ProductQueryModel model)
        {
            //分页
            var paging = model.Paging();

            var query = Db.Find();
            query.WhereIf(model.Title.NotNull(), m => m.Title.Contains(model.Title));

            //设置默认排序
            if (!paging.OrderBy.Any())
            {
                query.OrderByDescending(m => m.Id);
            }

            var list = await query.LeftJoin<AccountEntity>((x, y) => x.CreatedBy == y.Id)
                .Select((x, y) => new { x, Creator = y.Name })
                .PaginationAsync(paging);

            model.TotalCount = paging.TotalCount;
            return list;
        }

        /// <summary>
        /// 批量修改状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public Task<bool> UpdateStatus(List<Guid> ids, ProductStatus status)
        {
            return Db.Find(m => ids.Contains(m.Id)).UpdateAsync(m => new ProductEntity { Status = status });
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Task<bool> Delete(string title)
        {
            Db.Find().GroupBy(m => new { m.Status }).Select(m => new { m.Key.Status, Count = m.Count() });
        }
    }
}
