using System;
using System.Collections.Generic;
using System.Text;
using Nm.Lib.Data.Abstractions.Attributes;

namespace Nm.Module.Blog.Domain.Product
{
public partial class ProductEntity
    {
    /// <summary>
    /// sku列表
    /// </summary>
    [Ignore]
    public List<Guid> Skus { get; set; }
}
}
