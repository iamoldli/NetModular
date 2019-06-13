using System;
using Nm.Lib.Data.Abstractions.Attributes;
using Nm.Lib.Data.Core.Entities.Extend;

namespace Nm.Module.PersonnelFiles.Domain.Company
{
    /// <summary>
    /// 公司单位
    /// </summary>
    [Table("Company")]
    public partial class CompanyEntity : EntityBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

    }
}
