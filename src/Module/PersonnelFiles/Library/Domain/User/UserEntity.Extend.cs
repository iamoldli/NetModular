using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Utils.Core.Extensions;

namespace Nm.Module.PersonnelFiles.Domain.User
{
    public partial class UserEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Ignore]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [Ignore]
        public string PositionName { get; set; }

        /// <summary>
        /// 性别名称
        /// </summary>
        [Ignore]
        public string SexName => Sex.ToDescription();
    }
}
