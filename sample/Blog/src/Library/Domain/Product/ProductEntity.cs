using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.Blog.Domain.Product
{
    [Table("Product")]
    public partial class ProductEntity : EntityBase
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Store { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ProductStatus Status { get; set; }
    }
}
