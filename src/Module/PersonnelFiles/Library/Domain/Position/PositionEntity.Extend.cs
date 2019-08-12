using Tm.Lib.Data.Abstractions.Attributes;

namespace Tm.Module.PersonnelFiles.Domain.Position
{
    public partial class PositionEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [Ignore]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 公司单位
        /// </summary>
        [Ignore]
        public string CompanyName { get; set; }
    }
}
