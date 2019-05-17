using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.Blog.Domain.Category.Models
{
    public class CategoryQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
