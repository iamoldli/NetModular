using System;
using System.ComponentModel.DataAnnotations;
using NetModular.Lib.Data.Query;

namespace NetModular.Module.CodeGenerator.Domain.Class.Models
{
    public class ClassQueryModel : QueryModel
    {
        /// <summary>
        /// 所有项目
        /// </summary>
        [Required(ErrorMessage = "选择项目")]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
