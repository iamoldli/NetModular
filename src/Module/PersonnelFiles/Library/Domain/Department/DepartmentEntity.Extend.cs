using Tm.Lib.Data.Abstractions.Attributes;

namespace Tm.Module.PersonnelFiles.Domain.Department
{
    public partial class DepartmentEntity
    {
        /// <summary>
        /// ����������
        /// </summary>
        [Ignore]
        public string LeaderName { get; set; }
    }
}
