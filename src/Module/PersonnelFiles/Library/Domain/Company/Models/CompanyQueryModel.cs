using  Nm.Lib.Data.Query;
using System;

namespace  Nm.Module.PersonnelFiles.Domain.Company.Models
{
    public class CompanyQueryModel : QueryModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        public Guid AccountID { get; set; }

    }
}
