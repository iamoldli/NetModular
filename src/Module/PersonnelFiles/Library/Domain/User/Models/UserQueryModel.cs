using System;
using  Nm.Lib.Data.Query;

namespace  Nm.Module.PersonnelFiles.Domain.User.Models
{
    public class UserQueryModel : QueryModel
    {
        /// <summary>
        /// 职位编号
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string Number { get; set; }

    }
}
