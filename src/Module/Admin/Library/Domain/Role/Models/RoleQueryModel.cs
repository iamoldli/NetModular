using Tm.Lib.Data.Query;
using System;

namespace Tm.Module.Admin.Domain.Role.Models
{
    public class RoleQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 归属公司ID
        /// </summary>
        public Guid CID { get; set; }
    }
}
