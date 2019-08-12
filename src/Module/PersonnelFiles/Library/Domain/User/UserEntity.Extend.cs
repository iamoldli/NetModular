using Tm.Lib.Data.Abstractions.Attributes;
using Tm.Lib.Utils.Core.Extensions;

namespace Tm.Module.PersonnelFiles.Domain.User
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
