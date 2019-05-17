using System;
using System.ComponentModel.DataAnnotations;
using Nm.Lib.Data.Query;

namespace Nm.Module.CodeGenerator.Domain.Property.Models
{
    public class PropertyQueryModel : QueryModel
    {
        /// <summary>
        /// 类编号
        /// </summary>
        [Required(ErrorMessage = "请选择类")]
        public Guid ClassId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
