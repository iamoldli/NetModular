using Tm.Lib.Data.Abstractions.Attributes;

namespace Tm.Module.PersonnelFiles.Domain.Department
{
    public partial class DepartmentEntity
    {
        /// <summary>
        /// 负责人名称
        /// </summary>
        [Ignore]
        public string LeaderName { get; set; }
    }
}
