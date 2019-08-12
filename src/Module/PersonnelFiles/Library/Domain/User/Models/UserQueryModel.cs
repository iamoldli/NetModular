using Tm.Lib.Data.Query;
using System;

namespace Tm.Module.PersonnelFiles.Domain.User.Models
{
    public class UserQueryModel : QueryModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public int? Number { get; set; }


        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CID { get; set; } = Guid.Empty;
    }
}
