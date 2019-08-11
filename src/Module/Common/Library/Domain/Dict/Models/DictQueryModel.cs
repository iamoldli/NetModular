using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.Common.Domain.Dict.Models
{
    public class DictQueryModel : QueryModel
    {
        /// <summary>
        /// 父节点
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
