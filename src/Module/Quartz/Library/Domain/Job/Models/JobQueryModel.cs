using System;
using  Tm.Lib.Data.Query;

namespace  Tm.Module.Quartz.Domain.Job.Models
{
    public class JobQueryModel : QueryModel
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
    }
}
