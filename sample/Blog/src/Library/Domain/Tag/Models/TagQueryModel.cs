using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.Blog.Domain.Tag.Models
{
    public class TagQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

    }
}
